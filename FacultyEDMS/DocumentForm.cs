﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacultyEDMS
{
    public partial class DocumentForm : Form
    {

        public DocumentForm(int documentId, bool ReadOnly, int loggedInUserId, int loggedInUserRoleId, string loggedInUserRoleName)
        {
            InitializeComponent();
        }
    }
}
    