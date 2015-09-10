namespace Fashion.ERP.Reporting.Producao
{
    partial class MateriaisProgramacaoProducaoSubReport
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.detail = new Telerik.Reporting.DetailSection();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(1.6999999284744263D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pictureBox1,
            this.textBox24,
            this.textBox23,
            this.textBox22,
            this.textBox21,
            this.textBox20,
            this.textBox19,
            this.textBox18,
            this.textBox5,
            this.textBox2,
            this.textBox3});
            this.detail.Name = "detail";
            this.detail.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.detail.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.20000000298023224D);
            this.detail.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Bindings.Add(new Telerik.Reporting.Binding("Value", "= FashionErp.EnderecoFoto(Fields.NomeFoto)"));
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.6058334112167358D), Telerik.Reporting.Drawing.Unit.Cm(1.5029081106185913D));
            this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
            // 
            // textBox24
            // 
            this.textBox24.CanGrow = true;
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.09999942779541D), Telerik.Reporting.Drawing.Unit.Cm(1.0001997947692871D));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.6706345081329346D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox24.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox24.StyleName = "Caption";
            this.textBox24.Value = "Qtde. Material Programada";
            // 
            // textBox23
            // 
            this.textBox23.CanGrow = true;
            this.textBox23.Format = "{0:N5}";
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.799999237060547D), Telerik.Reporting.Drawing.Unit.Cm(1.0001997947692871D));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1998987197875977D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox23.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox23.StyleName = "Data";
            this.textBox23.Value = "=Fields.QuantidadeMaterial";
            // 
            // textBox22
            // 
            this.textBox22.CanGrow = true;
            this.textBox22.Format = "{0:N5}";
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.799999237060547D), Telerik.Reporting.Drawing.Unit.Cm(0.49749141931533813D));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1998987197875977D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox22.StyleName = "Data";
            this.textBox22.Value = "=Fields.QuantidadeConsumo";
            // 
            // textBox21
            // 
            this.textBox21.CanGrow = true;
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.09999942779541D), Telerik.Reporting.Drawing.Unit.Cm(0.49749141931533813D));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.6706345081329346D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox21.StyleName = "Caption";
            this.textBox21.Value = "Qtde. Consumo";
            // 
            // textBox20
            // 
            this.textBox20.CanGrow = true;
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.606033205986023D), Telerik.Reporting.Drawing.Unit.Cm(1.0029081106185913D));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.5529166460037231D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox20.StyleName = "Caption";
            this.textBox20.Value = "Und.";
            // 
            // textBox19
            // 
            this.textBox19.CanGrow = true;
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.1670749187469482D), Telerik.Reporting.Drawing.Unit.Cm(1.0029081106185913D));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.63292407989502D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox19.StyleName = "Data";
            this.textBox19.Value = "=Fields.UnidadeMedida";
            // 
            // textBox18
            // 
            this.textBox18.CanGrow = true;
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.1670749187469482D), Telerik.Reporting.Drawing.Unit.Cm(0.50019979476928711D));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.6329250335693359D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox18.StyleName = "Data";
            this.textBox18.Value = "=Fields.Descricao";
            // 
            // textBox5
            // 
            this.textBox5.CanGrow = true;
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.606033205986023D), Telerik.Reporting.Drawing.Unit.Cm(0.50019979476928711D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.5529166460037231D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox5.StyleName = "Caption";
            this.textBox5.Value = "Descrição";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.1670749187469482D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5468833446502686D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.StyleName = "Data";
            this.textBox2.Value = "=Fields.Referencia";
            // 
            // textBox3
            // 
            this.textBox3.CanGrow = true;
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.606033205986023D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.5529166460037231D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox3.StyleName = "Caption";
            this.textBox3.Value = "Referência";
            // 
            // MateriaisProgramacaoProducaoSubReport
            // 
            this.Bindings.Add(new Telerik.Reporting.Binding("DataSource", "=ReportItem.DataObject.Materiais"));
            this.ExternalStyleSheets.Add(new Telerik.Reporting.Drawing.ExternalStyleSheet("Fashion.ERP.Reporting.Resources.StyleSheet.xml"));
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.detail});
            this.Name = "DetalheModeloReport";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(5D), Telerik.Reporting.Drawing.Unit.Mm(5D), Telerik.Reporting.Drawing.Unit.Mm(10D), Telerik.Reporting.Drawing.Unit.Mm(5D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(19D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.PictureBox pictureBox1;

    }
}