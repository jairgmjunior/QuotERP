namespace Fashion.ERP.Reporting.Almoxarifado
{
    partial class ListaEstoqueMaterialReport
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.referenciaCaptionTextBox = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.EstoqueMaterialDataSource = new Telerik.Reporting.ObjectDataSource();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
            this.titleTextBox = new Telerik.Reporting.TextBox();
            this.reportFooter = new Telerik.Reporting.ReportFooterSection();
            this.Filtros = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.referenciaDataTextBox = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.550000011920929D);
            this.labelsGroupFooterSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox15,
            this.textBox14});
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = false;
            // 
            // textBox15
            // 
            this.textBox15.CanGrow = true;
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(18.094581604003906D), Telerik.Reporting.Drawing.Unit.Cm(0.049899648874998093D));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.89979809522628784D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox15.StyleName = "Caption";
            this.textBox15.Value = "Qtd:";
            // 
            // textBox14
            // 
            this.textBox14.CanGrow = true;
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(18.994579315185547D), Telerik.Reporting.Drawing.Unit.Cm(0.049899648874998093D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.95541971921920776D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox14.StyleName = "Caption";
            this.textBox14.Value = "= Count(Fields.Id)";
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.550000011920929D);
            this.labelsGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.referenciaCaptionTextBox,
            this.textBox1,
            this.textBox2,
            this.textBox3,
            this.textBox4,
            this.textBox5});
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // referenciaCaptionTextBox
            // 
            this.referenciaCaptionTextBox.CanGrow = true;
            this.referenciaCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.04999997466802597D), Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D));
            this.referenciaCaptionTextBox.Name = "referenciaCaptionTextBox";
            this.referenciaCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.9500001072883606D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.referenciaCaptionTextBox.StyleName = "Caption";
            this.referenciaCaptionTextBox.Value = "U.E.";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.0001999139785767D), Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.4996004104614258D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "Depósito";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.5000004768371582D), Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.4997998476028442D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox2.StyleName = "Caption";
            this.textBox2.Value = "Referência";
            // 
            // textBox3
            // 
            this.textBox3.CanGrow = true;
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8D), Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.882359504699707D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox3.StyleName = "Caption";
            this.textBox3.Value = "Descrição";
            // 
            // textBox4
            // 
            this.textBox4.CanGrow = true;
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.882560729980469D), Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7174391746520996D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox4.StyleName = "Caption";
            this.textBox4.Value = "Und. Medida";
            // 
            // textBox5
            // 
            this.textBox5.CanGrow = true;
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.600198745727539D), Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.3498003482818604D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox5.StyleName = "Caption";
            this.textBox5.Value = "Saldo";
            // 
            // EstoqueMaterialDataSource
            // 
            this.EstoqueMaterialDataSource.DataSource = typeof(Fashion.ERP.Domain.Almoxarifado.EstoqueMaterial);
            this.EstoqueMaterialDataSource.Name = "EstoqueMaterialDataSource";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox12,
            this.textBox11});
            this.pageHeader.Name = "pageHeader";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.050000190734863D), Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.8999996185302734D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox12.StyleName = "PageInfo";
            this.textBox12.Value = "=NOW()";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D), Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.8999996185302734D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox11.StyleName = "PageInfo";
            this.textBox11.Value = "Fashion Consultoria & Sistemas";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.currentTimeTextBox,
            this.pageInfoTextBox});
            this.pageFooter.Name = "pageFooter";
            // 
            // currentTimeTextBox
            // 
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.8999996185302734D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "=\"Fashion ERP © \" + Now().Year";
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.050000190734863D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.8999996185302734D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "= PageNumber";
            // 
            // reportHeader
            // 
            this.reportHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(1D);
            this.reportHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.titleTextBox});
            this.reportHeader.Name = "reportHeader";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.04999997466802597D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(19.899999618530273D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.titleTextBox.StyleName = "Title";
            this.titleTextBox.Value = "Consulta Estoque de Material";
            // 
            // reportFooter
            // 
            this.reportFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D);
            this.reportFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.Filtros});
            this.reportFooter.Name = "reportFooter";
            // 
            // Filtros
            // 
            this.Filtros.CanGrow = true;
            this.Filtros.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.04999997466802597D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.Filtros.Name = "Filtros";
            this.Filtros.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(19.899999618530273D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.Filtros.StyleName = "Caption";
            this.Filtros.Value = "= Parameters.Filtros.Value";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.550000011920929D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox10,
            this.textBox9,
            this.textBox8,
            this.textBox7,
            this.textBox6,
            this.referenciaDataTextBox});
            this.detail.Name = "detail";
            // 
            // textBox10
            // 
            this.textBox10.CanGrow = true;
            this.textBox10.Format = "{0:#.0000}";
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.600198745727539D), Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.3498003482818604D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox10.StyleName = "Data";
            this.textBox10.Value = "=Fields.Quantidade";
            // 
            // textBox9
            // 
            this.textBox9.CanGrow = true;
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.882560729980469D), Telerik.Reporting.Drawing.Unit.Cm(0.050000175833702087D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7174391746520996D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox9.StyleName = "Data";
            this.textBox9.Value = "=Fields.Material.UnidadeMedida.Sigla";
            // 
            // textBox8
            // 
            this.textBox8.CanGrow = true;
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8D), Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.8823604583740234D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox8.StyleName = "Data";
            this.textBox8.Value = "=Fields.Material.Descricao";
            // 
            // textBox7
            // 
            this.textBox7.CanGrow = true;
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.5000004768371582D), Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.4902162551879883D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox7.StyleName = "Data";
            this.textBox7.Value = "=Fields.Material.Referencia";
            // 
            // textBox6
            // 
            this.textBox6.CanGrow = true;
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.0001999139785767D), Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.4995999336242676D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox6.StyleName = "Data";
            this.textBox6.Value = "=Fields.DepositoMaterial.Nome";
            // 
            // referenciaDataTextBox
            // 
            this.referenciaDataTextBox.CanGrow = true;
            this.referenciaDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D), Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D));
            this.referenciaDataTextBox.Name = "referenciaDataTextBox";
            this.referenciaDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.9500001072883606D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.referenciaDataTextBox.StyleName = "Data";
            this.referenciaDataTextBox.Value = "=Fields.DepositoMaterial.Unidade.Unidade.Codigo";
            // 
            // ListaEstoqueMaterialReport
            // 
            this.DataSource = this.EstoqueMaterialDataSource;
            this.ExternalStyleSheets.Add(new Telerik.Reporting.Drawing.ExternalStyleSheet("Fashion.ERP.Reporting.Resources.StyleSheet.xml"));
            group1.GroupFooter = this.labelsGroupFooterSection;
            group1.GroupHeader = this.labelsGroupHeaderSection;
            group1.Name = "labelsGroup";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.pageHeader,
            this.pageFooter,
            this.reportHeader,
            this.reportFooter,
            this.detail});
            this.Name = "ListaEstoqueMaterialReport";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(5D), Telerik.Reporting.Drawing.Unit.Mm(5D), Telerik.Reporting.Drawing.Unit.Mm(10D), Telerik.Reporting.Drawing.Unit.Mm(5D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AllowNull = true;
            reportParameter1.Name = "Filtros";
            this.ReportParameters.Add(reportParameter1);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(20D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.ObjectDataSource EstoqueMaterialDataSource;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.ReportHeaderSection reportHeader;
        private Telerik.Reporting.ReportFooterSection reportFooter;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private Telerik.Reporting.TextBox titleTextBox;
        private Telerik.Reporting.TextBox Filtros;
        private Telerik.Reporting.TextBox referenciaCaptionTextBox;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox referenciaDataTextBox;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox14;

    }
}