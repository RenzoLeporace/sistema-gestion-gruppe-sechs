namespace UI
{
    partial class RolesYpermisos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label5 = new Label();
            BtnQuitarPermisoAUsuario = new Button();
            BtnAsociarPermisoAUsuario = new Button();
            checkBox1 = new CheckBox();
            checkBox3 = new CheckBox();
            label6 = new Label();
            label7 = new Label();
            textBox1 = new TextBox();
            BtnAltaRol = new Button();
            BtnModificarRol = new Button();
            BtnEliminarRol = new Button();
            BtnQuitarPermisoArol = new Button();
            BtnAsociarPermisoArol = new Button();
            BtnLimpiarCampos = new Button();
            BtnSalir = new Button();
            treeViewUsuarios = new TreeView();
            treeViewRoles = new TreeView();
            treeViewPermisos = new TreeView();
            treeViewPermisosPorRol = new TreeView();
            treeViewRolesYPermisosUsuario = new TreeView();
            txtUsuarioId = new TextBox();
            txtUsuarioNombre = new TextBox();
            txtUsuarioPassword = new TextBox();
            DesasociarRolesAUsuarios = new Button();
            BtnAsociarRolesaUsuarios = new Button();
            btnCrearUser = new Button();
            btnAsociarRolARol = new Button();
            btnQuitarRolHijo = new Button();
            label4 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            button1 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(26, 48);
            label1.Name = "label1";
            label1.Size = new Size(27, 20);
            label1.TabIndex = 5;
            label1.Text = "ID:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(26, 95);
            label2.Name = "label2";
            label2.Size = new Size(67, 20);
            label2.TabIndex = 6;
            label2.Text = "Nombre:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(26, 143);
            label3.Name = "label3";
            label3.Size = new Size(73, 20);
            label3.TabIndex = 7;
            label3.Text = "Password:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(163, 226);
            label5.Name = "label5";
            label5.Size = new Size(142, 20);
            label5.TabIndex = 9;
            label5.Text = "Permisos a Usuarios:";
            // 
            // BtnQuitarPermisoAUsuario
            // 
            BtnQuitarPermisoAUsuario.Location = new Point(255, 266);
            BtnQuitarPermisoAUsuario.Name = "BtnQuitarPermisoAUsuario";
            BtnQuitarPermisoAUsuario.Size = new Size(166, 74);
            BtnQuitarPermisoAUsuario.TabIndex = 13;
            BtnQuitarPermisoAUsuario.Text = "Quitar Permiso a Usuario";
            BtnQuitarPermisoAUsuario.UseVisualStyleBackColor = true;
            BtnQuitarPermisoAUsuario.Click += BtnQuitarPermisoAUsuario_Click;
            // 
            // BtnAsociarPermisoAUsuario
            // 
            BtnAsociarPermisoAUsuario.Location = new Point(69, 266);
            BtnAsociarPermisoAUsuario.Name = "BtnAsociarPermisoAUsuario";
            BtnAsociarPermisoAUsuario.Size = new Size(161, 74);
            BtnAsociarPermisoAUsuario.TabIndex = 12;
            BtnAsociarPermisoAUsuario.Text = "Asociar Permiso a Usuario";
            BtnAsociarPermisoAUsuario.UseVisualStyleBackColor = true;
            BtnAsociarPermisoAUsuario.Click += BtnAsociarPermisoAUsuario_Click;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new Point(255, 49);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(104, 24);
            checkBox1.TabIndex = 14;
            checkBox1.Text = "Bloqueado";
            checkBox1.UseVisualStyleBackColor = true;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Location = new Point(255, 95);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(73, 24);
            checkBox3.TabIndex = 16;
            checkBox3.Text = "Activo";
            checkBox3.UseVisualStyleBackColor = true;
            checkBox3.CheckedChanged += checkBox3_CheckedChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(529, 160);
            label6.Name = "label6";
            label6.Size = new Size(27, 20);
            label6.TabIndex = 17;
            label6.Text = "ID:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(675, 160);
            label7.Name = "label7";
            label7.Size = new Size(67, 20);
            label7.TabIndex = 18;
            label7.Text = "Nombre:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(759, 157);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(125, 27);
            textBox1.TabIndex = 19;
            // 
            // BtnAltaRol
            // 
            BtnAltaRol.Location = new Point(529, 212);
            BtnAltaRol.Name = "BtnAltaRol";
            BtnAltaRol.Size = new Size(94, 29);
            BtnAltaRol.TabIndex = 20;
            BtnAltaRol.Text = "Alta";
            BtnAltaRol.UseVisualStyleBackColor = true;
            BtnAltaRol.Click += BtnAltaRol_Click;
            // 
            // BtnModificarRol
            // 
            BtnModificarRol.Location = new Point(661, 212);
            BtnModificarRol.Name = "BtnModificarRol";
            BtnModificarRol.Size = new Size(94, 29);
            BtnModificarRol.TabIndex = 21;
            BtnModificarRol.Text = "Modificar";
            BtnModificarRol.UseVisualStyleBackColor = true;
            BtnModificarRol.Click += BtnModificarRol_Click;
            // 
            // BtnEliminarRol
            // 
            BtnEliminarRol.Location = new Point(790, 212);
            BtnEliminarRol.Name = "BtnEliminarRol";
            BtnEliminarRol.Size = new Size(94, 29);
            BtnEliminarRol.TabIndex = 22;
            BtnEliminarRol.Text = "Eliminar";
            BtnEliminarRol.UseVisualStyleBackColor = true;
            BtnEliminarRol.Click += BtnEliminarRol_Click;
            // 
            // BtnQuitarPermisoArol
            // 
            BtnQuitarPermisoArol.Location = new Point(1048, 462);
            BtnQuitarPermisoArol.Name = "BtnQuitarPermisoArol";
            BtnQuitarPermisoArol.Size = new Size(94, 74);
            BtnQuitarPermisoArol.TabIndex = 37;
            BtnQuitarPermisoArol.Text = "Quitar Permiso a Rol";
            BtnQuitarPermisoArol.UseVisualStyleBackColor = true;
            BtnQuitarPermisoArol.Click += BtnQuitarPermisoArol_Click;
            // 
            // BtnAsociarPermisoArol
            // 
            BtnAsociarPermisoArol.Location = new Point(1048, 382);
            BtnAsociarPermisoArol.Name = "BtnAsociarPermisoArol";
            BtnAsociarPermisoArol.Size = new Size(94, 74);
            BtnAsociarPermisoArol.TabIndex = 36;
            BtnAsociarPermisoArol.Text = "Asociar Permiso a Rol";
            BtnAsociarPermisoArol.UseVisualStyleBackColor = true;
            BtnAsociarPermisoArol.Click += BtnAsociarPermisoArol_Click;
            // 
            // BtnLimpiarCampos
            // 
            BtnLimpiarCampos.Location = new Point(1048, 551);
            BtnLimpiarCampos.Name = "BtnLimpiarCampos";
            BtnLimpiarCampos.Size = new Size(94, 56);
            BtnLimpiarCampos.TabIndex = 38;
            BtnLimpiarCampos.Text = "Limpiar Campos";
            BtnLimpiarCampos.UseVisualStyleBackColor = true;
            BtnLimpiarCampos.Click += BtnLimpiarCampos_Click;
            // 
            // BtnSalir
            // 
            BtnSalir.Location = new Point(1048, 642);
            BtnSalir.Name = "BtnSalir";
            BtnSalir.Size = new Size(94, 29);
            BtnSalir.TabIndex = 39;
            BtnSalir.Text = "Exit";
            BtnSalir.UseVisualStyleBackColor = true;
            BtnSalir.Click += BtnSalir_Click;
            // 
            // treeViewUsuarios
            // 
            treeViewUsuarios.Location = new Point(43, 371);
            treeViewUsuarios.Name = "treeViewUsuarios";
            treeViewUsuarios.Size = new Size(181, 308);
            treeViewUsuarios.TabIndex = 42;
            treeViewUsuarios.AfterSelect += treeViewUsuarios_AfterSelect_1;
            // 
            // treeViewRoles
            // 
            treeViewRoles.Location = new Point(285, 371);
            treeViewRoles.Name = "treeViewRoles";
            treeViewRoles.Size = new Size(202, 308);
            treeViewRoles.TabIndex = 43;
            treeViewRoles.AfterSelect += treeViewRoles_AfterSelect_1;
            // 
            // treeViewPermisos
            // 
            treeViewPermisos.Location = new Point(529, 371);
            treeViewPermisos.Name = "treeViewPermisos";
            treeViewPermisos.Size = new Size(253, 308);
            treeViewPermisos.TabIndex = 44;
            treeViewPermisos.AfterSelect += treeViewPermisos_AfterSelect;
            // 
            // treeViewPermisosPorRol
            // 
            treeViewPermisosPorRol.Location = new Point(817, 371);
            treeViewPermisosPorRol.Name = "treeViewPermisosPorRol";
            treeViewPermisosPorRol.Size = new Size(206, 308);
            treeViewPermisosPorRol.TabIndex = 45;
            // 
            // treeViewRolesYPermisosUsuario
            // 
            treeViewRolesYPermisosUsuario.Location = new Point(908, 31);
            treeViewRolesYPermisosUsuario.Name = "treeViewRolesYPermisosUsuario";
            treeViewRolesYPermisosUsuario.Size = new Size(234, 308);
            treeViewRolesYPermisosUsuario.TabIndex = 46;
            // 
            // txtUsuarioId
            // 
            txtUsuarioId.Location = new Point(69, 46);
            txtUsuarioId.Name = "txtUsuarioId";
            txtUsuarioId.ReadOnly = true;
            txtUsuarioId.Size = new Size(125, 27);
            txtUsuarioId.TabIndex = 47;
            // 
            // txtUsuarioNombre
            // 
            txtUsuarioNombre.Location = new Point(99, 92);
            txtUsuarioNombre.Name = "txtUsuarioNombre";
            txtUsuarioNombre.ReadOnly = true;
            txtUsuarioNombre.Size = new Size(125, 27);
            txtUsuarioNombre.TabIndex = 48;
            // 
            // txtUsuarioPassword
            // 
            txtUsuarioPassword.Location = new Point(105, 140);
            txtUsuarioPassword.Name = "txtUsuarioPassword";
            txtUsuarioPassword.ReadOnly = true;
            txtUsuarioPassword.Size = new Size(125, 27);
            txtUsuarioPassword.TabIndex = 6;
            // 
            // DesasociarRolesAUsuarios
            // 
            DesasociarRolesAUsuarios.Location = new Point(759, 266);
            DesasociarRolesAUsuarios.Name = "DesasociarRolesAUsuarios";
            DesasociarRolesAUsuarios.Size = new Size(125, 74);
            DesasociarRolesAUsuarios.TabIndex = 29;
            DesasociarRolesAUsuarios.Text = "Desasociar Roles a Usuario";
            DesasociarRolesAUsuarios.UseVisualStyleBackColor = true;
            DesasociarRolesAUsuarios.Click += DesasociarRolesAUsuarios_Click;
            // 
            // BtnAsociarRolesaUsuarios
            // 
            BtnAsociarRolesaUsuarios.Location = new Point(529, 266);
            BtnAsociarRolesaUsuarios.Name = "BtnAsociarRolesaUsuarios";
            BtnAsociarRolesaUsuarios.Size = new Size(136, 74);
            BtnAsociarRolesaUsuarios.TabIndex = 28;
            BtnAsociarRolesaUsuarios.Text = "Asociar Roles a Usuario";
            BtnAsociarRolesaUsuarios.UseVisualStyleBackColor = true;
            BtnAsociarRolesaUsuarios.Click += BtnAsociarRolesaUsuarios_Click;
            // 
            // btnCrearUser
            // 
            btnCrearUser.Location = new Point(26, 183);
            btnCrearUser.Name = "btnCrearUser";
            btnCrearUser.Size = new Size(168, 29);
            btnCrearUser.TabIndex = 49;
            btnCrearUser.Text = "Crear Usuario";
            btnCrearUser.UseVisualStyleBackColor = true;
            btnCrearUser.Click += btnCrearUser_Click;
            // 
            // btnAsociarRolARol
            // 
            btnAsociarRolARol.Location = new Point(571, 80);
            btnAsociarRolARol.Name = "btnAsociarRolARol";
            btnAsociarRolARol.Size = new Size(94, 29);
            btnAsociarRolARol.TabIndex = 50;
            btnAsociarRolARol.Text = "Asociar Rol A Rol";
            btnAsociarRolARol.UseVisualStyleBackColor = true;
            btnAsociarRolARol.Click += btnAsociarRolARol_Click_1;
            // 
            // btnQuitarRolHijo
            // 
            btnQuitarRolHijo.Location = new Point(733, 80);
            btnQuitarRolHijo.Name = "btnQuitarRolHijo";
            btnQuitarRolHijo.Size = new Size(94, 29);
            btnQuitarRolHijo.TabIndex = 51;
            btnQuitarRolHijo.Text = "Quitar Rol Hijo";
            btnQuitarRolHijo.UseVisualStyleBackColor = true;
            btnQuitarRolHijo.Click += btnQuitarRolHijo_Click_1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(43, 348);
            label4.Name = "label4";
            label4.Size = new Size(68, 20);
            label4.TabIndex = 52;
            label4.Text = "Usuarios:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(285, 348);
            label8.Name = "label8";
            label8.Size = new Size(48, 20);
            label8.TabIndex = 53;
            label8.Text = "Roles:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(607, 37);
            label9.Name = "label9";
            label9.Size = new Size(195, 20);
            label9.TabIndex = 54;
            label9.Text = "Control y Creacion de Roles:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(529, 348);
            label10.Name = "label10";
            label10.Size = new Size(70, 20);
            label10.TabIndex = 55;
            label10.Text = "Permisos:";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(814, 348);
            label11.Name = "label11";
            label11.Size = new Size(121, 20);
            label11.TabIndex = 56;
            label11.Text = "Permisos Por Rol:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(908, 8);
            label12.Name = "label12";
            label12.Size = new Size(199, 20);
            label12.TabIndex = 57;
            label12.Text = "Roles Y Permisos del usuario:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(26, 8);
            label13.Name = "label13";
            label13.Size = new Size(215, 20);
            label13.TabIndex = 58;
            label13.Text = "Control y Creacion de Usuarios:";
            // 
            // button1
            // 
            button1.Location = new Point(200, 183);
            button1.Name = "button1";
            button1.Size = new Size(182, 29);
            button1.TabIndex = 59;
            button1.Text = "Eliminar Usuario";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // RolesYpermisos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1155, 683);
            Controls.Add(button1);
            Controls.Add(label13);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label4);
            Controls.Add(btnQuitarRolHijo);
            Controls.Add(btnAsociarRolARol);
            Controls.Add(btnCrearUser);
            Controls.Add(txtUsuarioPassword);
            Controls.Add(txtUsuarioNombre);
            Controls.Add(txtUsuarioId);
            Controls.Add(treeViewRolesYPermisosUsuario);
            Controls.Add(treeViewPermisosPorRol);
            Controls.Add(treeViewPermisos);
            Controls.Add(treeViewRoles);
            Controls.Add(treeViewUsuarios);
            Controls.Add(BtnSalir);
            Controls.Add(BtnLimpiarCampos);
            Controls.Add(BtnQuitarPermisoArol);
            Controls.Add(BtnAsociarPermisoArol);
            Controls.Add(DesasociarRolesAUsuarios);
            Controls.Add(BtnAsociarRolesaUsuarios);
            Controls.Add(BtnEliminarRol);
            Controls.Add(BtnModificarRol);
            Controls.Add(BtnAltaRol);
            Controls.Add(textBox1);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(checkBox3);
            Controls.Add(checkBox1);
            Controls.Add(BtnQuitarPermisoAUsuario);
            Controls.Add(BtnAsociarPermisoAUsuario);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "RolesYpermisos";
            Text = "RolesYpermisos";
            Load += RolesYpermisos_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label5;
        private Button BtnQuitarPermisoAUsuario;
        private Button BtnAsociarPermisoAUsuario;
        private CheckBox checkBox1;
        private CheckBox checkBox3;
        private Label label6;
        private Label label7;
        private TextBox textBox1;
        private Button BtnAltaRol;
        private Button BtnModificarRol;
        private Button BtnEliminarRol;
        private Button BtnQuitarPermisoArol;
        private Button BtnAsociarPermisoArol;
        private Button BtnLimpiarCampos;
        private Button BtnSalir;
        private TreeView treeViewUsuarios;
        private TreeView treeViewRoles;
        private TreeView treeViewPermisos;
        private TreeView treeViewPermisosPorRol;
        private TreeView treeViewRolesYPermisosUsuario;
        private TextBox txtUsuarioId;
        private TextBox txtUsuarioNombre;
        private TextBox txtUsuarioPassword;
        private Button DesasociarRolesAUsuarios;
        private Button BtnAsociarRolesaUsuarios;
        private Button btnCrearUser;
        private Button btnAsociarRolARol;
        private Button btnQuitarRolHijo;
        private Label label4;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Button button1;
    }
}