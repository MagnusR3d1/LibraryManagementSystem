﻿
namespace FoodTicketingSystem
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.logout_btn = new System.Windows.Forms.Button();
            this.AddItem = new System.Windows.Forms.Button();
            this.dashboard_btn = new System.Windows.Forms.Button();
            this.greet_label = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Dashboard = new FoodTicketingSystem.Dashboard();
            this.Addfoods = new FoodTicketingSystem.Addfoods();
            this.MenuItems = new FoodTicketingSystem.MenuItems();
            this.FufillOrders = new FoodTicketingSystem.FufillOrders();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(65)))), ((int)(((byte)(68)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1467, 40);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(17, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(319, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Food Ticketing System | Admin Form";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1443, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "X";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(65)))), ((int)(((byte)(68)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.logout_btn);
            this.panel2.Controls.Add(this.AddItem);
            this.panel2.Controls.Add(this.dashboard_btn);
            this.panel2.Controls.Add(this.greet_label);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 40);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(293, 652);
            this.panel2.TabIndex = 1;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(65, 613);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 21);
            this.label4.TabIndex = 7;
            this.label4.Text = "Log out";
            // 
            // logout_btn
            // 
            this.logout_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logout_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.logout_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSeaGreen;
            this.logout_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logout_btn.ForeColor = System.Drawing.Color.White;
            this.logout_btn.Image = global::FoodTicketingSystem.Properties.Resources.icons8_logout_rounded_up_filled_20px;
            this.logout_btn.Location = new System.Drawing.Point(11, 602);
            this.logout_btn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.logout_btn.Name = "logout_btn";
            this.logout_btn.Size = new System.Drawing.Size(47, 40);
            this.logout_btn.TabIndex = 6;
            this.logout_btn.UseVisualStyleBackColor = true;
            this.logout_btn.Click += new System.EventHandler(this.logout_btn_Click);
            // 
            // AddItem
            // 
            this.AddItem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddItem.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.AddItem.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSeaGreen;
            this.AddItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddItem.ForeColor = System.Drawing.Color.White;
            this.AddItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AddItem.Location = new System.Drawing.Point(11, 272);
            this.AddItem.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.AddItem.Name = "AddItem";
            this.AddItem.Size = new System.Drawing.Size(267, 52);
            this.AddItem.TabIndex = 3;
            this.AddItem.Text = "ADD ITEM";
            this.AddItem.UseVisualStyleBackColor = true;
            this.AddItem.Click += new System.EventHandler(this.addItems_btn_Click);
            // 
            // dashboard_btn
            // 
            this.dashboard_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.dashboard_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DarkSeaGreen;
            this.dashboard_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkSeaGreen;
            this.dashboard_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dashboard_btn.ForeColor = System.Drawing.Color.White;
            this.dashboard_btn.Image = global::FoodTicketingSystem.Properties.Resources.icons8_dashboard_32px;
            this.dashboard_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dashboard_btn.Location = new System.Drawing.Point(11, 213);
            this.dashboard_btn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dashboard_btn.Name = "dashboard_btn";
            this.dashboard_btn.Size = new System.Drawing.Size(267, 52);
            this.dashboard_btn.TabIndex = 2;
            this.dashboard_btn.Text = "DASHBOARD";
            this.dashboard_btn.UseVisualStyleBackColor = true;
            this.dashboard_btn.Click += new System.EventHandler(this.dashboard_btn_Click);
            // 
            // greet_label
            // 
            this.greet_label.AutoSize = true;
            this.greet_label.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.greet_label.ForeColor = System.Drawing.Color.White;
            this.greet_label.Location = new System.Drawing.Point(55, 151);
            this.greet_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.greet_label.Name = "greet_label";
            this.greet_label.Size = new System.Drawing.Size(160, 24);
            this.greet_label.TabIndex = 1;
            this.greet_label.Text = "Welcome, Admin";
            this.greet_label.Click += new System.EventHandler(this.greet_label_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(77, 32);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(133, 115);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.Dashboard);
            this.panel3.Controls.Add(this.Addfoods);
            this.panel3.Controls.Add(this.MenuItems);
            this.panel3.Controls.Add(this.FufillOrders);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(293, 40);
            this.panel3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1174, 652);
            this.panel3.TabIndex = 2;
            // 
            // Dashboard
            // 
            this.Dashboard.Location = new System.Drawing.Point(0, 0);
            this.Dashboard.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.Dashboard.Name = "Dashboard";
            this.Dashboard.Size = new System.Drawing.Size(1173, 652);
            this.Dashboard.TabIndex = 3;
            this.Dashboard.Load += new System.EventHandler(this.dashboard1_Load);
            // 
            // Addfoods
            // 
            this.Addfoods.Location = new System.Drawing.Point(0, 0);
            this.Addfoods.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.Addfoods.Name = "Addfoods";
            this.Addfoods.Size = new System.Drawing.Size(1173, 652);
            this.Addfoods.TabIndex = 2;
            // 
            // MenuItems
            // 
            this.MenuItems.Location = new System.Drawing.Point(0, 0);
            this.MenuItems.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.MenuItems.Name = "MenuItems";
            this.MenuItems.Size = new System.Drawing.Size(1173, 652);
            this.MenuItems.TabIndex = 1;
            // 
            // FufillOrders
            // 
            this.FufillOrders.Location = new System.Drawing.Point(0, 0);
            this.FufillOrders.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.FufillOrders.Name = "FufillOrders";
            this.FufillOrders.Size = new System.Drawing.Size(1173, 652);
            this.FufillOrders.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1467, 692);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button AddItem;
        private System.Windows.Forms.Button dashboard_btn;
        private System.Windows.Forms.Label greet_label;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button logout_btn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel3;
        private FufillOrders FufillOrders;
        private Addfoods Addfoods;
        private MenuItems MenuItems;
        private Dashboard Dashboard;
    }
}