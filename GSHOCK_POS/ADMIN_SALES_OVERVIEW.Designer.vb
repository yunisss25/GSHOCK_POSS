<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ADMIN_SALES_OVERVIEW
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ADMIN_SALES_OVERVIEW))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.MP = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.INVENTORY = New System.Windows.Forms.Button()
        Me.MONTHLY = New System.Windows.Forms.Button()
        Me.WEEKLY = New System.Windows.Forms.Button()
        Me.DAILY = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.C3 = New System.Windows.Forms.Button()
        Me.C2 = New System.Windows.Forms.Button()
        Me.C1 = New System.Windows.Forms.Button()
        Me.Panel3.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(275, 75)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(452, 56)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "SALES OVERVIEW"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 36.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(416, 131)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(213, 56)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "(ADMIN)"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Panel3.Controls.Add(Me.PictureBox1)
        Me.Panel3.Controls.Add(Me.MP)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(995, 47)
        Me.Panel3.TabIndex = 15
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.ErrorImage = CType(resources.GetObject("PictureBox1.ErrorImage"), System.Drawing.Image)
        Me.PictureBox1.InitialImage = CType(resources.GetObject("PictureBox1.InitialImage"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(0, 0)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(127, 47)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'MP
        '
        Me.MP.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.MP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.MP.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MP.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.MP.Location = New System.Drawing.Point(857, 12)
        Me.MP.Name = "MP"
        Me.MP.Size = New System.Drawing.Size(126, 24)
        Me.MP.TabIndex = 0
        Me.MP.Text = "MAIN PAGE"
        Me.MP.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.Panel1.Controls.Add(Me.Button3)
        Me.Panel1.Controls.Add(Me.INVENTORY)
        Me.Panel1.Controls.Add(Me.MONTHLY)
        Me.Panel1.Controls.Add(Me.WEEKLY)
        Me.Panel1.Controls.Add(Me.DAILY)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 47)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(180, 544)
        Me.Panel1.TabIndex = 16
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(3, 340)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(174, 45)
        Me.Button3.TabIndex = 5
        Me.Button3.Text = "USERS"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'INVENTORY
        '
        Me.INVENTORY.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.INVENTORY.Location = New System.Drawing.Point(3, 289)
        Me.INVENTORY.Name = "INVENTORY"
        Me.INVENTORY.Size = New System.Drawing.Size(174, 45)
        Me.INVENTORY.TabIndex = 4
        Me.INVENTORY.Text = "INVENTORY"
        Me.INVENTORY.UseVisualStyleBackColor = True
        '
        'MONTHLY
        '
        Me.MONTHLY.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MONTHLY.Location = New System.Drawing.Point(3, 148)
        Me.MONTHLY.Name = "MONTHLY"
        Me.MONTHLY.Size = New System.Drawing.Size(174, 45)
        Me.MONTHLY.TabIndex = 2
        Me.MONTHLY.Text = "MONTHLY"
        Me.MONTHLY.UseVisualStyleBackColor = True
        '
        'WEEKLY
        '
        Me.WEEKLY.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WEEKLY.Location = New System.Drawing.Point(3, 97)
        Me.WEEKLY.Name = "WEEKLY"
        Me.WEEKLY.Size = New System.Drawing.Size(174, 45)
        Me.WEEKLY.TabIndex = 1
        Me.WEEKLY.Text = "WEEKLY"
        Me.WEEKLY.UseVisualStyleBackColor = True
        '
        'DAILY
        '
        Me.DAILY.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DAILY.Location = New System.Drawing.Point(3, 46)
        Me.DAILY.Name = "DAILY"
        Me.DAILY.Size = New System.Drawing.Size(174, 45)
        Me.DAILY.TabIndex = 0
        Me.DAILY.Text = "DAILY"
        Me.DAILY.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.Panel2.Controls.Add(Me.C3)
        Me.Panel2.Controls.Add(Me.C2)
        Me.Panel2.Controls.Add(Me.C1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel2.Location = New System.Drawing.Point(815, 47)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(180, 544)
        Me.Panel2.TabIndex = 17
        '
        'C3
        '
        Me.C3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C3.Location = New System.Drawing.Point(3, 144)
        Me.C3.Name = "C3"
        Me.C3.Size = New System.Drawing.Size(174, 45)
        Me.C3.TabIndex = 6
        Me.C3.Text = "CASHIER 3"
        Me.C3.UseVisualStyleBackColor = True
        '
        'C2
        '
        Me.C2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C2.Location = New System.Drawing.Point(3, 93)
        Me.C2.Name = "C2"
        Me.C2.Size = New System.Drawing.Size(174, 45)
        Me.C2.TabIndex = 5
        Me.C2.Text = "CASHIER 2"
        Me.C2.UseVisualStyleBackColor = True
        '
        'C1
        '
        Me.C1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.C1.Location = New System.Drawing.Point(3, 42)
        Me.C1.Name = "C1"
        Me.C1.Size = New System.Drawing.Size(174, 45)
        Me.C1.TabIndex = 4
        Me.C1.Text = "CASHIER 1"
        Me.C1.UseVisualStyleBackColor = True
        '
        'ADMIN_SALES_OVERVIEW
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(995, 591)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "ADMIN_SALES_OVERVIEW"
        Me.Text = "ADMIN_OVERALL"
        Me.Panel3.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents MP As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Button3 As Button
    Friend WithEvents INVENTORY As Button
    Friend WithEvents MONTHLY As Button
    Friend WithEvents WEEKLY As Button
    Friend WithEvents DAILY As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents C3 As Button
    Friend WithEvents C2 As Button
    Friend WithEvents C1 As Button
End Class
