namespace Fashion.ERP.Reporting.EngenhariaProduto
{
    partial class SolicitacaoMaterialCompraReport
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.InstanceReportSource instanceReportSource1 = new Telerik.Reporting.InstanceReportSource();
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            this.solicitacaoMaterialCompraSubReport1 = new Fashion.ERP.Reporting.EngenhariaProduto.SolicitacaoMaterialCompraSubReport();
            this.groupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.Quantidade = new Telerik.Reporting.TextBox();
            this.groupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.MarcaSolicitacaoMaterialCompraDataSource = new Telerik.Reporting.ObjectDataSource();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
            this.titleTextBox = new Telerik.Reporting.TextBox();
            this.reportFooter = new Telerik.Reporting.ReportFooterSection();
            this.Filtros = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.subReport1 = new Telerik.Reporting.SubReport();
            ((System.ComponentModel.ISupportInitialize)(this.solicitacaoMaterialCompraSubReport1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // solicitacaoMaterialCompraSubReport1
            // 
            this.solicitacaoMaterialCompraSubReport1.Name = "DetalheModeloReport";
            // 
            // groupFooterSection
            // 
            this.groupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.50019943714141846D);
            this.groupFooterSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.Quantidade});
            this.groupFooterSection.Name = "groupFooterSection";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.899999618530273D), Telerik.Reporting.Drawing.Unit.Cm(0.00019943872757721692D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.4002010822296143D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "Registros:";
            // 
            // Quantidade
            // 
            this.Quantidade.CanGrow = true;
            this.Quantidade.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(18.30040168762207D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.Quantidade.Name = "Quantidade";
            this.Quantidade.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.6494953632354736D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.Quantidade.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.Quantidade.StyleName = "Caption";
            this.Quantidade.Value = "= FashionErp.CountList(Fields.Materiais)";
            // 
            // groupHeaderSection
            // 
            this.groupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(1.1002002954483032D);
            this.groupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox11,
            this.textBox18,
            this.textBox5});
            this.groupHeaderSection.Name = "groupHeaderSection";
            // 
            // textBox11
            // 
            this.textBox11.CanGrow = true;
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.1000000238418579D), Telerik.Reporting.Drawing.Unit.Cm(0.600200355052948D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.4997987747192383D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox11.StyleName = "Data";
            this.textBox11.Value = "=Fields.Marca";
            // 
            // textBox18
            // 
            this.textBox18.CanGrow = true;
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.049800131469964981D), Telerik.Reporting.Drawing.Unit.Cm(0.60010063648223877D));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.0500000715255737D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox18.StyleName = "Caption";
            this.textBox18.Value = "Marca";
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(19.950000762939453D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox5.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox5.StyleName = "GroupPanel";
            this.textBox5.Value = "Dados da Marca";
            // 
            // MarcaSolicitacaoMaterialCompraDataSource
            // 
            this.MarcaSolicitacaoMaterialCompraDataSource.DataSource = typeof(Fashion.ERP.Reporting.EngenhariaProduto.Models.MarcaSolicitacaoMaterialCompraModel);
            this.MarcaSolicitacaoMaterialCompraDataSource.Name = "MarcaSolicitacaoMaterialCompraDataSource";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(0.60000008344650269D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox13,
            this.textBox14});
            this.pageHeader.Name = "pageHeader";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.050000190734863D), Telerik.Reporting.Drawing.Unit.Cm(0.050000026822090149D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.8999996185302734D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox13.StyleName = "PageInfo";
            this.textBox13.Value = "=NOW()";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D), Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.8999996185302734D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox14.StyleName = "PageInfo";
            this.textBox14.Value = "Fashion Consultoria & Sistemas";
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
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D), Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.8999996185302734D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "=\"Fashion ERP © \" + Now().Year";
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.050000190734863D), Telerik.Reporting.Drawing.Unit.Cm(0.050000026822090149D));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.8999996185302734D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "= PageNumber";
            // 
            // reportHeader
            // 
            this.reportHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D);
            this.reportHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.titleTextBox});
            this.reportHeader.Name = "reportHeader";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(19.94999885559082D), Telerik.Reporting.Drawing.Unit.Cm(0.99989998340606689D));
            this.titleTextBox.StyleName = "Title";
            this.titleTextBox.Value = "Solicitação de Material Para Compra";
            // 
            // reportFooter
            // 
            this.reportFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.47354167699813843D);
            this.reportFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.Filtros});
            this.reportFooter.Name = "reportFooter";
            // 
            // Filtros
            // 
            this.Filtros.CanGrow = true;
            this.Filtros.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(-0.026458332315087318D));
            this.Filtros.Name = "Filtros";
            this.Filtros.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(19.896982192993164D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.Filtros.StyleName = "Caption";
            this.Filtros.Value = "= Parameters.Filtros.Value";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(2.7998001575469971D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.subReport1});
            this.detail.Name = "detail";
            this.detail.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.detail.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.None;
            // 
            // subReport1
            // 
            this.subReport1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(-5.6458848707308107E-10D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.subReport1.Name = "subReport1";
            instanceReportSource1.ReportDocument = this.solicitacaoMaterialCompraSubReport1;
            this.subReport1.ReportSource = instanceReportSource1;
            this.subReport1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(19.949899673461914D), Telerik.Reporting.Drawing.Unit.Cm(2.7995998859405518D));
            // 
            // SolicitacaoMaterialCompraReport
            // 
            this.DataSource = this.MarcaSolicitacaoMaterialCompraDataSource;
            this.ExternalStyleSheets.Add(new Telerik.Reporting.Drawing.ExternalStyleSheet("Fashion.ERP.Reporting.Resources.StyleSheet.xml"));
            group1.GroupFooter = this.groupFooterSection;
            group1.GroupHeader = this.groupHeaderSection;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.Marca"));
            group1.Name = "group2";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection,
            this.groupFooterSection,
            this.pageHeader,
            this.pageFooter,
            this.reportHeader,
            this.reportFooter,
            this.detail});
            this.Name = "DetalheModeloReport";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(5D), Telerik.Reporting.Drawing.Unit.Mm(5D), Telerik.Reporting.Drawing.Unit.Mm(10D), Telerik.Reporting.Drawing.Unit.Mm(5D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AllowNull = true;
            reportParameter1.Name = "Filtros";
            this.ReportParameters.Add(reportParameter1);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(19.94999885559082D);
            ((System.ComponentModel.ISupportInitialize)(this.solicitacaoMaterialCompraSubReport1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.ObjectDataSource MarcaSolicitacaoMaterialCompraDataSource;
        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.ReportHeaderSection reportHeader;
        private Telerik.Reporting.ReportFooterSection reportFooter;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox titleTextBox;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private Telerik.Reporting.TextBox Filtros;
        private Telerik.Reporting.GroupHeaderSection groupHeaderSection;
        private Telerik.Reporting.GroupFooterSection groupFooterSection;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.SubReport subReport1;
        private SolicitacaoMaterialCompraSubReport solicitacaoMaterialCompraSubReport1;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox Quantidade;

    }
}