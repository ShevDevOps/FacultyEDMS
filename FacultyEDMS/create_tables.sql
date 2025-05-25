-- Use your database (replace 'department_docs' with your DB name)
USE department_docs;

-- Drop existing tables (use with caution!)
DROP TABLE IF EXISTS logs;
DROP TABLE IF EXISTS notifications;
DROP TABLE IF EXISTS documentversions;
DROP TABLE IF EXISTS documents;
DROP TABLE IF EXISTS role_document_permissions;
DROP TABLE IF EXISTS documenttypes;
DROP TABLE IF EXISTS users;
DROP TABLE IF EXISTS roles;

-- Create the roles table
CREATE TABLE roles (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(50) NOT NULL UNIQUE,
    -- General administrative permissions (not tied to document types)
    can_manage_users BOOLEAN DEFAULT FALSE
);

-- Create the users table
CREATE TABLE users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL UNIQUE,
    password VARCHAR(255) NOT NULL, -- Store ONLY password hashes!
    role_id INT NOT NULL,
    isBlocked TINYINT(1) DEFAULT 0,
    FOREIGN KEY (role_id) REFERENCES roles(id)
);

-- Create the documenttypes table
CREATE TABLE documenttypes (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL UNIQUE
);

-- Create the role_document_permissions table
CREATE TABLE role_document_permissions (
    role_id INT NOT NULL,
    document_type_id INT NOT NULL,
    can_create BOOLEAN DEFAULT FALSE,
    can_edit_own BOOLEAN DEFAULT FALSE,
    can_edit_all BOOLEAN DEFAULT FALSE,
    can_sign BOOLEAN DEFAULT FALSE,
    can_view_all BOOLEAN DEFAULT FALSE,
    PRIMARY KEY (role_id, document_type_id),
    FOREIGN KEY (role_id) REFERENCES roles(id) ON DELETE CASCADE,
    FOREIGN KEY (document_type_id) REFERENCES documenttypes(id) ON DELETE CASCADE
);

-- Create the documents table
CREATE TABLE documents (
    id INT AUTO_INCREMENT PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    description TEXT NULL,
    authorId INT NOT NULL,
    type_id INT NOT NULL,
    createdAt DATETIME NOT NULL,
    updatedAt DATETIME NULL,
    isSigned TINYINT(1) DEFAULT 0,
    isProtected TINYINT(1) DEFAULT 0,
    filePath VARCHAR(1024) NULL,
    FOREIGN KEY (authorId) REFERENCES users(id),
    FOREIGN KEY (type_id) REFERENCES documenttypes(id)
);

-- Create the documentversions table
CREATE TABLE documentversions (
    id INT AUTO_INCREMENT PRIMARY KEY,
    document_id INT NOT NULL,
    versionNumber INT NOT NULL,
    changeSummary TEXT,
    filePath VARCHAR(1024) NOT NULL,
    createdAt DATETIME NOT NULL,
    editorId INT NOT NULL,
    FOREIGN KEY (document_id) REFERENCES documents(id) ON DELETE CASCADE,
    FOREIGN KEY (editorId) REFERENCES users(id)
);

-- Create the notifications table
CREATE TABLE notifications (
    id INT AUTO_INCREMENT PRIMARY KEY,
    receiverId INT NOT NULL,
    message TEXT NOT NULL,
    createdAt DATETIME NOT NULL,
    isRead TINYINT(1) DEFAULT 0,
    document_id INT NULL,
    FOREIGN KEY (receiverId) REFERENCES users(id),
    FOREIGN KEY (document_id) REFERENCES documents(id) ON DELETE SET NULL
);

-- Create the logs table
CREATE TABLE logs (
    id INT AUTO_INCREMENT PRIMARY KEY,
    userId INT NULL,
    action VARCHAR(255) NOT NULL,
    target VARCHAR(255),
    timestamp DATETIME NOT NULL,
    FOREIGN KEY (userId) REFERENCES users(id) ON DELETE SET NULL
);

-- Create indexes
CREATE UNIQUE INDEX idx_email ON users(email);
CREATE INDEX idx_role_id ON users(role_id);
CREATE INDEX idx_author_id ON documents(authorId);
CREATE INDEX idx_type_id ON documents(type_id);
CREATE FULLTEXT INDEX idx_title_fulltext ON documents(title);
CREATE INDEX idx_document_id_versions ON documentversions(document_id);
CREATE INDEX idx_receiver_id_notifications ON notifications(receiverId);
CREATE INDEX idx_user_id_logs ON logs(userId);

-- Insert base data (Roles)
INSERT INTO roles (name, can_manage_users) VALUES
('Student', FALSE),
('Teacher', FALSE),
('Head of Department', FALSE),
('Administrator', TRUE);

-- Insert base data (Document Types)
INSERT INTO documenttypes (name) VALUES
('Application'),
('Order'),
('Protocol'),
('Memo');

-- Insert permissions for each role and document type
-- Student permissions
INSERT INTO role_document_permissions (role_id, document_type_id, can_create, can_edit_own, can_edit_all, can_sign, can_view_all) VALUES
((SELECT id FROM roles WHERE name = 'Student'), (SELECT id FROM documenttypes WHERE name = 'Application'), TRUE, TRUE, FALSE, FALSE, FALSE),
((SELECT id FROM roles WHERE name = 'Student'), (SELECT id FROM documenttypes WHERE name = 'Order'), FALSE, FALSE, FALSE, FALSE, FALSE),
((SELECT id FROM roles WHERE name = 'Student'), (SELECT id FROM documenttypes WHERE name = 'Protocol'), FALSE, FALSE, FALSE, FALSE, FALSE),
((SELECT id FROM roles WHERE name = 'Student'), (SELECT id FROM documenttypes WHERE name = 'Memo'), FALSE, FALSE, FALSE, FALSE, FALSE);

-- Teacher permissions
INSERT INTO role_document_permissions (role_id, document_type_id, can_create, can_edit_own, can_edit_all, can_sign, can_view_all) VALUES
((SELECT id FROM roles WHERE name = 'Teacher'), (SELECT id FROM documenttypes WHERE name = 'Application'), TRUE, TRUE, FALSE, FALSE, FALSE),
((SELECT id FROM roles WHERE name = 'Teacher'), (SELECT id FROM documenttypes WHERE name = 'Order'), TRUE, TRUE, FALSE, TRUE, FALSE),
((SELECT id FROM roles WHERE name = 'Teacher'), (SELECT id FROM documenttypes WHERE name = 'Protocol'), TRUE, TRUE, FALSE, TRUE, FALSE),
((SELECT id FROM roles WHERE name = 'Teacher'), (SELECT id FROM documenttypes WHERE name = 'Memo'), TRUE, TRUE, FALSE, TRUE, FALSE);

-- Head of Department permissions
INSERT INTO role_document_permissions (role_id, document_type_id, can_create, can_edit_own, can_edit_all, can_sign, can_view_all) VALUES
((SELECT id FROM roles WHERE name = 'Head of Department'), (SELECT id FROM documenttypes WHERE name = 'Application'), FALSE, FALSE, FALSE, TRUE, TRUE),
((SELECT id FROM roles WHERE name = 'Head of Department'), (SELECT id FROM documenttypes WHERE name = 'Order'), FALSE, FALSE, FALSE, TRUE, TRUE),
((SELECT id FROM roles WHERE name = 'Head of Department'), (SELECT id FROM documenttypes WHERE name = 'Protocol'), FALSE, FALSE, FALSE, TRUE, TRUE),
((SELECT id FROM roles WHERE name = 'Head of Department'), (SELECT id FROM documenttypes WHERE name = 'Memo'), FALSE, FALSE, FALSE, TRUE, TRUE);

-- Administrator permissions (can do everything with all document types)
INSERT INTO role_document_permissions (role_id, document_type_id, can_create, can_edit_own, can_edit_all, can_sign, can_view_all)
SELECT r.id, dt.id, TRUE, TRUE, TRUE, TRUE, TRUE
FROM roles r, documenttypes dt
WHERE r.name = 'Administrator';

-- Add example users
-- IMPORTANT: Replace 'admin_password_hash', 'student_password_hash', 'teacher_password_hash' with REAL password hashes!
INSERT INTO users (username, email, password, role_id, isBlocked) VALUES
('Admin User', 'admin@fit.knu.ua', '6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b', (SELECT id FROM roles WHERE name = 'Administrator'), 0);

INSERT INTO users (username, email, password, role_id, isBlocked) VALUES
('Student User', 'student@fit.knu.ua', '6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b', (SELECT id FROM roles WHERE name = 'Student'), 0);

INSERT INTO users (username, email, password, role_id, isBlocked) VALUES
('Teacher User', 'teacher@fit.knu.ua', '6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b', (SELECT id FROM roles WHERE name = 'Teacher'), 0);

SELECT 'Database creation script executed successfully.' AS message;