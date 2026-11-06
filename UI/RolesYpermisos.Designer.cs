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
            panelUsuarios = new Panel();
            panelRoles = new Panel();
            panelPermisos = new Panel();
            panelAsignados = new Panel();
            tituloPagina = new Label();
            ayudaPagina = new Label();
            ayudaSeleccion = new Label();
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
            // Cuatro áreas visibles: usuarios, roles, catálogo y asignaciones.
            tituloPagina.Text = "Roles y permisos";
            tituloPagina.Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold);
            tituloPagina.AutoSize = true;
            tituloPagina.Location = new Point(22, 15);
            ayudaPagina.Text = "Seleccioná un usuario o rol; marcá permisos para asociarlos o quitarlos.";
            ayudaPagina.AutoSize = true;
            ayudaPagina.Location = new Point(25, 65);
            ayudaSeleccion.Text = "Los cambios se aplican al usuario y rol seleccionados.";
            ayudaSeleccion.AutoSize = true;
            ayudaSeleccion.Location = new Point(15, 685);

            panelUsuarios.Location = new Point(20, 105);
            panelUsuarios.Size = new Size(330, 760);
            panelUsuarios.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            panelRoles.Location = new Point(365, 105);
            panelRoles.Size = new Size(330, 760);
            panelRoles.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            panelPermisos.Location = new Point(710, 105);
            panelPermisos.Size = new Size(330, 760);
            panelPermisos.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            panelAsignados.Location = new Point(1055, 105);
            panelAsignados.Size = new Size(380, 760);
            panelAsignados.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            panelUsuarios.Controls.AddRange(new Control[] { label13, label1, txtUsuarioId, label2, txtUsuarioNombre,
                label3, txtUsuarioPassword, checkBox1, checkBox3, btnCrearUser, button1, label4, treeViewUsuarios });
            panelRoles.Controls.AddRange(new Control[] { label9, label6, label7, textBox1, BtnAltaRol,
                BtnModificarRol, BtnEliminarRol, label8, treeViewRoles, btnAsociarRolARol, btnQuitarRolHijo });
            panelPermisos.Controls.AddRange(new Control[] { label10, treeViewPermisos, label5,
                BtnAsociarPermisoAUsuario, BtnQuitarPermisoAUsuario, BtnAsociarPermisoArol, BtnQuitarPermisoArol, ayudaSeleccion });
            panelAsignados.Controls.AddRange(new Control[] { label12, treeViewRolesYPermisosUsuario,
                BtnAsociarRolesaUsuarios, DesasociarRolesAUsuarios, label11, treeViewPermisosPorRol,
                BtnLimpiarCampos, BtnSalir });

            label13.Text = "01  Usuarios"; label13.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold); label13.Location = new Point(15, 15);
            label1.Location = new Point(15, 65); txtUsuarioId.Location = new Point(100, 62); txtUsuarioId.Size = new Size(195, 27);
            label2.Location = new Point(15, 105); txtUsuarioNombre.Location = new Point(100, 102); txtUsuarioNombre.Size = new Size(195, 27);
            label3.Text = "Clave:"; label3.Location = new Point(15, 145); txtUsuarioPassword.Location = new Point(100, 142); txtUsuarioPassword.Size = new Size(195, 27);
            checkBox1.Location = new Point(15, 185); checkBox3.Location = new Point(170, 185);
            btnCrearUser.Text = "Crear usuario"; btnCrearUser.Location = new Point(15, 225); btnCrearUser.Size = new Size(145, 40);
            button1.Text = "Eliminar usuario"; button1.Location = new Point(170, 225); button1.Size = new Size(145, 40);
            label4.Text = "Seleccioná un usuario"; label4.Location = new Point(15, 285);
            treeViewUsuarios.Location = new Point(15, 315); treeViewUsuarios.Size = new Size(300, 285);
            treeViewUsuarios.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            label9.Text = "02  Roles"; label9.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold); label9.Location = new Point(15, 15);
            label6.Location = new Point(15, 65); label6.Text = "ID automático";
            label7.Location = new Point(15, 105); textBox1.Location = new Point(100, 102); textBox1.Size = new Size(195, 27);
            BtnAltaRol.Text = "Crear"; BtnAltaRol.Location = new Point(15, 155); BtnAltaRol.Size = new Size(95, 40);
            BtnModificarRol.Text = "Guardar"; BtnModificarRol.Location = new Point(116, 155); BtnModificarRol.Size = new Size(95, 40);
            BtnEliminarRol.Text = "Eliminar"; BtnEliminarRol.Location = new Point(217, 155); BtnEliminarRol.Size = new Size(95, 40);
            label8.Text = "Seleccioná un rol"; label8.Location = new Point(15, 215);
            treeViewRoles.Location = new Point(15, 245); treeViewRoles.Size = new Size(300, 255);
            treeViewRoles.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnAsociarRolARol.Text = "Vincular rol"; btnAsociarRolARol.Location = new Point(15, 530); btnAsociarRolARol.Size = new Size(145, 40);
            btnQuitarRolHijo.Text = "Quitar vínculo"; btnQuitarRolHijo.Location = new Point(170, 530); btnQuitarRolHijo.Size = new Size(145, 40);

            label10.Text = "03  Permisos disponibles"; label10.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold); label10.Location = new Point(15, 15);
            treeViewPermisos.Location = new Point(15, 60); treeViewPermisos.Size = new Size(300, 300);
            treeViewPermisos.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label5.Text = "Aplicar permisos seleccionados"; label5.Location = new Point(15, 375);
            BtnAsociarPermisoAUsuario.Text = "Añadir al usuario"; BtnAsociarPermisoAUsuario.Location = new Point(15, 410); BtnAsociarPermisoAUsuario.Size = new Size(145, 40);
            BtnQuitarPermisoAUsuario.Text = "Quitar del usuario"; BtnQuitarPermisoAUsuario.Location = new Point(170, 410); BtnQuitarPermisoAUsuario.Size = new Size(145, 40);
            BtnAsociarPermisoArol.Text = "Añadir al rol"; BtnAsociarPermisoArol.Location = new Point(15, 465); BtnAsociarPermisoArol.Size = new Size(145, 40);
            BtnQuitarPermisoArol.Text = "Quitar del rol"; BtnQuitarPermisoArol.Location = new Point(170, 465); BtnQuitarPermisoArol.Size = new Size(145, 40);
            ayudaSeleccion.Location = new Point(15, 535);

            label12.Text = "04  Accesos del usuario"; label12.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold); label12.Location = new Point(15, 15);
            treeViewRolesYPermisosUsuario.Location = new Point(15, 60); treeViewRolesYPermisosUsuario.Size = new Size(350, 190);
            treeViewRolesYPermisosUsuario.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnAsociarRolesaUsuarios.Text = "Asignar rol al usuario"; BtnAsociarRolesaUsuarios.Location = new Point(15, 265); BtnAsociarRolesaUsuarios.Size = new Size(165, 42);
            DesasociarRolesAUsuarios.Text = "Quitar rol del usuario"; DesasociarRolesAUsuarios.Location = new Point(195, 265); DesasociarRolesAUsuarios.Size = new Size(170, 42);
            label11.Text = "Permisos del rol elegido"; label11.Location = new Point(15, 325);
            treeViewPermisosPorRol.Location = new Point(15, 350); treeViewPermisosPorRol.Size = new Size(350, 190);
            treeViewPermisosPorRol.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            BtnLimpiarCampos.Text = "Limpiar selección"; BtnLimpiarCampos.Location = new Point(15, 555); BtnLimpiarCampos.Size = new Size(165, 40);
            BtnSalir.Text = "Cerrar"; BtnSalir.Location = new Point(195, 555); BtnSalir.Size = new Size(170, 40);
            BtnLimpiarCampos.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            BtnSalir.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            Controls.Add(panelUsuarios);
            Controls.Add(panelRoles);
            Controls.Add(panelPermisos);
            Controls.Add(panelAsignados);
            Controls.Add(tituloPagina);
            Controls.Add(ayudaPagina);
            Name = "RolesYpermisos";
            Text = "Gruppe Sechs | Usuarios, roles y permisos";
            ClientSize = new Size(1455, 750);
            MinimumSize = new Size(980, 650);
            AutoScroll = true;
            AutoScrollMinSize = new Size(1455, 750);
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
        private Panel panelUsuarios;
        private Panel panelRoles;
        private Panel panelPermisos;
        private Panel panelAsignados;
        private Label tituloPagina;
        private Label ayudaPagina;
        private Label ayudaSeleccion;
    }
}
