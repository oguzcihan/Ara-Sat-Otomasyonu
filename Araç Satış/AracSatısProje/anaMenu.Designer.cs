namespace AracSatısProje
{
    partial class anaMenu
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(anaMenu));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dosyalarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kullanıcıDeğiştirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hesapMakinesiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kullanıcıİşlemleriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.müşteriKayıtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.personelKayıtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.işlemlerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.araçSatışToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.araçKayıtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.araçGüncelleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markaKayıtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.satışSilToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.servisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.araçServisiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yedekParçaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.raporToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aracRaporuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.satışRaporToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.servisRaporToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yedekParçaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.fabrikaBİlgiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.silinenMüşteriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayarlarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yedekleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblad = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblunvan = new System.Windows.Forms.Label();
            this.menuStrip3 = new System.Windows.Forms.MenuStrip();
            this.label4 = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.fabrikaKayıtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.toolStripButton1,
            this.toolStripButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 64);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1106, 35);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(82, 32);
            this.toolStripButton3.Text = "Araç Satış";
            this.toolStripButton3.Visible = false;
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click_2);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(83, 32);
            this.toolStripButton1.Text = "Araç Kayıt";
            this.toolStripButton1.Visible = false;
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(94, 32);
            this.toolStripButton2.Text = "Servis Kayıt";
            this.toolStripButton2.Visible = false;
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dosyalarToolStripMenuItem,
            this.kullanıcıİşlemleriToolStripMenuItem,
            this.işlemlerToolStripMenuItem,
            this.servisToolStripMenuItem,
            this.raporToolStripMenuItem,
            this.ayarlarToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 24);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1106, 40);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dosyalarToolStripMenuItem
            // 
            this.dosyalarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kullanıcıDeğiştirToolStripMenuItem,
            this.hesapMakinesiToolStripMenuItem});
            this.dosyalarToolStripMenuItem.Name = "dosyalarToolStripMenuItem";
            this.dosyalarToolStripMenuItem.Size = new System.Drawing.Size(74, 36);
            this.dosyalarToolStripMenuItem.Text = "Dosyalar";
            // 
            // kullanıcıDeğiştirToolStripMenuItem
            // 
            this.kullanıcıDeğiştirToolStripMenuItem.Name = "kullanıcıDeğiştirToolStripMenuItem";
            this.kullanıcıDeğiştirToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.kullanıcıDeğiştirToolStripMenuItem.Text = "Kullanıcı Değiştir";
            this.kullanıcıDeğiştirToolStripMenuItem.Click += new System.EventHandler(this.kullanıcıDeğiştirToolStripMenuItem_Click);
            // 
            // hesapMakinesiToolStripMenuItem
            // 
            this.hesapMakinesiToolStripMenuItem.Name = "hesapMakinesiToolStripMenuItem";
            this.hesapMakinesiToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.hesapMakinesiToolStripMenuItem.Text = "Hesap Makinesi";
            this.hesapMakinesiToolStripMenuItem.Click += new System.EventHandler(this.hesapMakinesiToolStripMenuItem_Click);
            // 
            // kullanıcıİşlemleriToolStripMenuItem
            // 
            this.kullanıcıİşlemleriToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.müşteriKayıtToolStripMenuItem,
            this.personelKayıtToolStripMenuItem});
            this.kullanıcıİşlemleriToolStripMenuItem.Name = "kullanıcıİşlemleriToolStripMenuItem";
            this.kullanıcıİşlemleriToolStripMenuItem.Size = new System.Drawing.Size(124, 36);
            this.kullanıcıİşlemleriToolStripMenuItem.Text = "Kullanıcı İşlemleri";
            // 
            // müşteriKayıtToolStripMenuItem
            // 
            this.müşteriKayıtToolStripMenuItem.Name = "müşteriKayıtToolStripMenuItem";
            this.müşteriKayıtToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.müşteriKayıtToolStripMenuItem.Text = "Müşteri Kayıt";
            this.müşteriKayıtToolStripMenuItem.Visible = false;
            this.müşteriKayıtToolStripMenuItem.Click += new System.EventHandler(this.müşteriKayıtToolStripMenuItem_Click);
            // 
            // personelKayıtToolStripMenuItem
            // 
            this.personelKayıtToolStripMenuItem.Name = "personelKayıtToolStripMenuItem";
            this.personelKayıtToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.personelKayıtToolStripMenuItem.Text = "Personel Kayıt";
            this.personelKayıtToolStripMenuItem.Visible = false;
            this.personelKayıtToolStripMenuItem.Click += new System.EventHandler(this.personelKayıtToolStripMenuItem_Click);
            // 
            // işlemlerToolStripMenuItem
            // 
            this.işlemlerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.araçSatışToolStripMenuItem,
            this.araçKayıtToolStripMenuItem,
            this.araçGüncelleToolStripMenuItem,
            this.markaKayıtToolStripMenuItem,
            this.satışSilToolStripMenuItem});
            this.işlemlerToolStripMenuItem.Name = "işlemlerToolStripMenuItem";
            this.işlemlerToolStripMenuItem.Size = new System.Drawing.Size(102, 36);
            this.işlemlerToolStripMenuItem.Text = "Araç İşlemleri";
            // 
            // araçSatışToolStripMenuItem
            // 
            this.araçSatışToolStripMenuItem.Name = "araçSatışToolStripMenuItem";
            this.araçSatışToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.araçSatışToolStripMenuItem.Text = "Araç Satış";
            this.araçSatışToolStripMenuItem.Visible = false;
            this.araçSatışToolStripMenuItem.Click += new System.EventHandler(this.araçSatışToolStripMenuItem_Click_1);
            // 
            // araçKayıtToolStripMenuItem
            // 
            this.araçKayıtToolStripMenuItem.Name = "araçKayıtToolStripMenuItem";
            this.araçKayıtToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.araçKayıtToolStripMenuItem.Text = "Araç Kayıt";
            this.araçKayıtToolStripMenuItem.Visible = false;
            this.araçKayıtToolStripMenuItem.Click += new System.EventHandler(this.araçKayıtToolStripMenuItem_Click_1);
            // 
            // araçGüncelleToolStripMenuItem
            // 
            this.araçGüncelleToolStripMenuItem.Name = "araçGüncelleToolStripMenuItem";
            this.araçGüncelleToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.araçGüncelleToolStripMenuItem.Text = "Araç Güncelle";
            this.araçGüncelleToolStripMenuItem.Visible = false;
            this.araçGüncelleToolStripMenuItem.Click += new System.EventHandler(this.araçGüncelleToolStripMenuItem_Click);
            // 
            // markaKayıtToolStripMenuItem
            // 
            this.markaKayıtToolStripMenuItem.Name = "markaKayıtToolStripMenuItem";
            this.markaKayıtToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.markaKayıtToolStripMenuItem.Text = "Marka Kayıt";
            this.markaKayıtToolStripMenuItem.Visible = false;
            this.markaKayıtToolStripMenuItem.Click += new System.EventHandler(this.markaKayıtToolStripMenuItem_Click);
            // 
            // satışSilToolStripMenuItem
            // 
            this.satışSilToolStripMenuItem.Name = "satışSilToolStripMenuItem";
            this.satışSilToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.satışSilToolStripMenuItem.Text = "Satış Sil";
            this.satışSilToolStripMenuItem.Visible = false;
            this.satışSilToolStripMenuItem.Click += new System.EventHandler(this.satışSilToolStripMenuItem_Click);
            // 
            // servisToolStripMenuItem
            // 
            this.servisToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.araçServisiToolStripMenuItem,
            this.yedekParçaToolStripMenuItem,
            this.fabrikaKayıtToolStripMenuItem});
            this.servisToolStripMenuItem.Name = "servisToolStripMenuItem";
            this.servisToolStripMenuItem.Size = new System.Drawing.Size(56, 36);
            this.servisToolStripMenuItem.Text = "Servis";
            // 
            // araçServisiToolStripMenuItem
            // 
            this.araçServisiToolStripMenuItem.Name = "araçServisiToolStripMenuItem";
            this.araçServisiToolStripMenuItem.Size = new System.Drawing.Size(151, 24);
            this.araçServisiToolStripMenuItem.Text = "Araç Servis";
            this.araçServisiToolStripMenuItem.Visible = false;
            this.araçServisiToolStripMenuItem.Click += new System.EventHandler(this.araçServisiToolStripMenuItem_Click);
            // 
            // yedekParçaToolStripMenuItem
            // 
            this.yedekParçaToolStripMenuItem.Name = "yedekParçaToolStripMenuItem";
            this.yedekParçaToolStripMenuItem.Size = new System.Drawing.Size(151, 24);
            this.yedekParçaToolStripMenuItem.Text = "Yedek Parça";
            this.yedekParçaToolStripMenuItem.Visible = false;
            this.yedekParçaToolStripMenuItem.Click += new System.EventHandler(this.yedekParçaToolStripMenuItem_Click);
            // 
            // raporToolStripMenuItem
            // 
            this.raporToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aracRaporuToolStripMenuItem,
            this.satışRaporToolStripMenuItem,
            this.servisRaporToolStripMenuItem,
            this.yedekParçaToolStripMenuItem1,
            this.fabrikaBİlgiToolStripMenuItem,
            this.silinenMüşteriToolStripMenuItem});
            this.raporToolStripMenuItem.Name = "raporToolStripMenuItem";
            this.raporToolStripMenuItem.Size = new System.Drawing.Size(57, 36);
            this.raporToolStripMenuItem.Text = "Rapor";
            // 
            // aracRaporuToolStripMenuItem
            // 
            this.aracRaporuToolStripMenuItem.Name = "aracRaporuToolStripMenuItem";
            this.aracRaporuToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.aracRaporuToolStripMenuItem.Text = "Arac Stok";
            this.aracRaporuToolStripMenuItem.Click += new System.EventHandler(this.aracRaporuToolStripMenuItem_Click);
            // 
            // satışRaporToolStripMenuItem
            // 
            this.satışRaporToolStripMenuItem.Name = "satışRaporToolStripMenuItem";
            this.satışRaporToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.satışRaporToolStripMenuItem.Text = "Arac Satış";
            this.satışRaporToolStripMenuItem.Click += new System.EventHandler(this.satışRaporToolStripMenuItem_Click);
            // 
            // servisRaporToolStripMenuItem
            // 
            this.servisRaporToolStripMenuItem.Name = "servisRaporToolStripMenuItem";
            this.servisRaporToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.servisRaporToolStripMenuItem.Text = "Araç Servis";
            this.servisRaporToolStripMenuItem.Click += new System.EventHandler(this.servisRaporToolStripMenuItem_Click);
            // 
            // yedekParçaToolStripMenuItem1
            // 
            this.yedekParçaToolStripMenuItem1.Name = "yedekParçaToolStripMenuItem1";
            this.yedekParçaToolStripMenuItem1.Size = new System.Drawing.Size(168, 24);
            this.yedekParçaToolStripMenuItem1.Text = "Yedek Parça";
            this.yedekParçaToolStripMenuItem1.Click += new System.EventHandler(this.yedekParçaToolStripMenuItem1_Click);
            // 
            // fabrikaBİlgiToolStripMenuItem
            // 
            this.fabrikaBİlgiToolStripMenuItem.Name = "fabrikaBİlgiToolStripMenuItem";
            this.fabrikaBİlgiToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.fabrikaBİlgiToolStripMenuItem.Text = "Fabrika Bilgi";
            this.fabrikaBİlgiToolStripMenuItem.Click += new System.EventHandler(this.fabrikaBİlgiToolStripMenuItem_Click);
            // 
            // silinenMüşteriToolStripMenuItem
            // 
            this.silinenMüşteriToolStripMenuItem.Name = "silinenMüşteriToolStripMenuItem";
            this.silinenMüşteriToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.silinenMüşteriToolStripMenuItem.Text = "Silinen Müşteri";
            this.silinenMüşteriToolStripMenuItem.Click += new System.EventHandler(this.silinenMüşteriToolStripMenuItem_Click);
            // 
            // ayarlarToolStripMenuItem
            // 
            this.ayarlarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.yedekleToolStripMenuItem,
            this.restoreToolStripMenuItem});
            this.ayarlarToolStripMenuItem.Name = "ayarlarToolStripMenuItem";
            this.ayarlarToolStripMenuItem.Size = new System.Drawing.Size(64, 36);
            this.ayarlarToolStripMenuItem.Text = "Ayarlar";
            this.ayarlarToolStripMenuItem.Visible = false;
            // 
            // yedekleToolStripMenuItem
            // 
            this.yedekleToolStripMenuItem.Name = "yedekleToolStripMenuItem";
            this.yedekleToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.yedekleToolStripMenuItem.Text = "Yedekle";
            this.yedekleToolStripMenuItem.Click += new System.EventHandler(this.yedekleToolStripMenuItem_Click);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.restoreToolStripMenuItem.Text = "Geri Yükle";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // menuStrip2
            // 
            this.menuStrip2.AutoSize = false;
            this.menuStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuStrip2.Location = new System.Drawing.Point(0, 466);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(1106, 34);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Menu;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = ".";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Menu;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(144, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "KULLANICI:";
            // 
            // lblad
            // 
            this.lblad.AutoSize = true;
            this.lblad.BackColor = System.Drawing.SystemColors.Menu;
            this.lblad.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblad.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblad.Location = new System.Drawing.Point(361, 8);
            this.lblad.Name = "lblad";
            this.lblad.Size = new System.Drawing.Size(14, 15);
            this.lblad.TabIndex = 5;
            this.lblad.Text = "a";
            this.lblad.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblad.Visible = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblunvan
            // 
            this.lblunvan.AutoSize = true;
            this.lblunvan.BackColor = System.Drawing.SystemColors.Menu;
            this.lblunvan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblunvan.Location = new System.Drawing.Point(213, 8);
            this.lblunvan.Name = "lblunvan";
            this.lblunvan.Size = new System.Drawing.Size(14, 15);
            this.lblunvan.TabIndex = 7;
            this.lblunvan.Text = "a";
            // 
            // menuStrip3
            // 
            this.menuStrip3.Location = new System.Drawing.Point(0, 0);
            this.menuStrip3.Name = "menuStrip3";
            this.menuStrip3.Size = new System.Drawing.Size(1106, 24);
            this.menuStrip3.TabIndex = 9;
            this.menuStrip3.Text = "menuStrip3";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Menu;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(294, 474);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(230, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "ARAÇ SATIŞ SİSTEMİ-2019 ®";
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 10;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // fabrikaKayıtToolStripMenuItem
            // 
            this.fabrikaKayıtToolStripMenuItem.Name = "fabrikaKayıtToolStripMenuItem";
            this.fabrikaKayıtToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
            this.fabrikaKayıtToolStripMenuItem.Text = "Fabrika Kayıt";
            this.fabrikaKayıtToolStripMenuItem.Click += new System.EventHandler(this.fabrikaKayıtToolStripMenuItem_Click);
            // 
            // anaMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(1106, 500);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblunvan);
            this.Controls.Add(this.lblad);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.menuStrip3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip2;
            this.Name = "anaMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Araç Satış-Anamenü";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.anaMenu_FormClosing);
            this.Load += new System.EventHandler(this.anaMenu_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dosyalarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kullanıcıDeğiştirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hesapMakinesiToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripMenuItem işlemlerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem araçSatışToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem araçKayıtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem araçGüncelleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markaKayıtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem servisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem araçServisiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yedekParçaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayarlarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yedekleToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton2;

        private System.Windows.Forms.ToolStripMenuItem raporToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aracRaporuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem satışRaporToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem servisRaporToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yedekParçaToolStripMenuItem1;

        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kullanıcıİşlemleriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem personelKayıtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem müşteriKayıtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem satışSilToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblad;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblunvan;
        private System.Windows.Forms.ToolStripMenuItem fabrikaBİlgiToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolStripMenuItem silinenMüşteriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fabrikaKayıtToolStripMenuItem;
    }
}