namespace Fashion.ERP.Reporting.Producao
{
    partial class MateriaisProgramacaoProducaoSemFotoSubReport
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.50009983777999878D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox23,
            this.textBox22,
            this.textBox19,
            this.textBox18,
            this.textBox2,
            this.textBox1});
            this.detail.Name = "detail";
            this.detail.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(0.20000000298023224D);
            this.detail.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            // 
            // textBox23
            // 
            this.textBox23.CanGrow = true;
            this.textBox23.Format = "{0:N5}";
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.100201606750488D), Telerik.Reporting.Drawing.Unit.Cm(9.9921220680698752E-05D));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.499798059463501D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox23.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox23.StyleName = "Data";
            this.textBox23.Value = "=Fields.QuantidadeMaterial";
            // 
            // textBox22
            // 
            this.textBox22.CanGrow = true;
            this.textBox22.Format = "{0:N5}";
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.853116989135742D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.2468838691711426D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox22.StyleName = "Data";
            this.textBox22.Value = "=Fields.QuantidadeConsumo";
            // 
            // textBox19
            // 
            this.textBox19.CanGrow = true;
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.653116226196289D), Telerik.Reporting.Drawing.Unit.Cm(9.9921220680698752E-05D));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.199799656867981D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox19.StyleName = "Data";
            this.textBox19.Value = "=Fields.UnidadeMedida";
            // 
            // textBox18
            // 
            this.textBox18.CanGrow = true;
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.606033205986023D), Telerik.Reporting.Drawing.Unit.Cm(9.9921220680698752E-05D));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.0468826293945312D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox18.StyleName = "Data";
            this.textBox18.Value = "=Fields.Descricao";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.00010002215276472271D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.6058332920074463D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox2.StyleName = "Data";
            this.textBox2.Value = "=Fields.Referencia";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Format = "{0:N5}";
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.899999618530273D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.0499999523162842D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox1.StyleName = "Data";
            this.textBox1.Value = "=Fields.Departamento";
            // 
            // MateriaisProgramacaoProducaoSemFotoSubReport
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
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox1;

    }
}