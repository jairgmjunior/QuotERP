namespace Fashion.ERP.Reporting.Producao
{
    partial class MateriaisProgramacaoProducaoReport
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
            this.materiaisProgramacaoProducaoSubReport1 = new Fashion.ERP.Reporting.Producao.MateriaisProgramacaoProducaoSubReport();
            this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.referenciaCaptionTextBox = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.referenciaDataTextBox = new Telerik.Reporting.TextBox();
            this.descricaoDataTextBox = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.ListaModeloDataSource = new Telerik.Reporting.ObjectDataSource();
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
            this.subReport1 = new Telerik.Reporting.SubReport();
            this.textBox46 = new Telerik.Reporting.TextBox();
            this.objectDataSourceMateriais = new Telerik.Reporting.ObjectDataSource();
            ((System.ComponentModel.ISupportInitialize)(this.materiaisProgramacaoProducaoSubReport1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // materiaisProgramacaoProducaoSubReport1
            // 
            this.materiaisProgramacaoProducaoSubReport1.Name = "DetalheModeloReport";
            // 
            // groupFooterSection1
            // 
            this.groupFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.550000011920929D);
            this.groupFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox15,
            this.textBox14});
            this.groupFooterSection1.Name = "groupFooterSection1";
            // 
            // textBox15
            // 
            this.textBox15.CanGrow = true;
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.499797821044922D), Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox15.StyleName = "Caption";
            this.textBox15.Value = "Qtd:";
            // 
            // textBox14
            // 
            this.textBox14.CanGrow = true;
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(18.099998474121094D), Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.85000139474868774D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox14.StyleName = "Caption";
            this.textBox14.Value = "= FashionErp.CountList(Fields.Materiais)";
            // 
            // groupHeaderSection1
            // 
            this.groupHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(1.6996997594833374D);
            this.groupHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox8,
            this.textBox10,
            this.textBox9,
            this.referenciaCaptionTextBox,
            this.textBox1,
            this.referenciaDataTextBox,
            this.descricaoDataTextBox,
            this.textBox4,
            this.textBox16,
            this.textBox6,
            this.textBox13});
            this.groupHeaderSection1.Name = "groupHeaderSection1";
            // 
            // textBox8
            // 
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(18.999898910522461D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox8.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Groove;
            this.textBox8.StyleName = "GroupPanel";
            this.textBox8.Value = "Dados da Programação de Produção";
            // 
            // textBox10
            // 
            this.textBox10.CanGrow = true;
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.600200355052948D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.5529166460037231D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox10.StyleName = "Caption";
            this.textBox10.Value = "Tag";
            // 
            // textBox9
            // 
            this.textBox9.CanGrow = true;
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(1.1004006862640381D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.8000000715255737D), Telerik.Reporting.Drawing.Unit.Cm(0.59929901361465454D));
            this.textBox9.Style.Font.Bold = true;
            this.textBox9.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox9.StyleName = "Data";
            this.textBox9.Value = "=Fields.Tag";
            // 
            // referenciaCaptionTextBox
            // 
            this.referenciaCaptionTextBox.CanGrow = true;
            this.referenciaCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(0.600200355052948D));
            this.referenciaCaptionTextBox.Name = "referenciaCaptionTextBox";
            this.referenciaCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.5529166460037231D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.referenciaCaptionTextBox.StyleName = "Caption";
            this.referenciaCaptionTextBox.Value = "Referência";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2D), Telerik.Reporting.Drawing.Unit.Cm(1.1004006862640381D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.5529166460037231D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "Descrição";
            // 
            // referenciaDataTextBox
            // 
            this.referenciaDataTextBox.CanGrow = true;
            this.referenciaDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.5531172752380371D), Telerik.Reporting.Drawing.Unit.Cm(0.600200355052948D));
            this.referenciaDataTextBox.Name = "referenciaDataTextBox";
            this.referenciaDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5468833446502686D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.referenciaDataTextBox.StyleName = "Data";
            this.referenciaDataTextBox.Value = "=Fields.Referencia";
            // 
            // descricaoDataTextBox
            // 
            this.descricaoDataTextBox.CanGrow = true;
            this.descricaoDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.5531165599823D), Telerik.Reporting.Drawing.Unit.Cm(1.1004006862640381D));
            this.descricaoDataTextBox.Name = "descricaoDataTextBox";
            this.descricaoDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.2468833923339844D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.descricaoDataTextBox.StyleName = "Data";
            this.descricaoDataTextBox.Value = "=Fields.Descricao";
            // 
            // textBox4
            // 
            this.textBox4.CanGrow = true;
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.200000762939453D), Telerik.Reporting.Drawing.Unit.Cm(0.600200355052948D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.2999992370605469D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox4.StyleName = "Caption";
            this.textBox4.Value = "Estilista";
            // 
            // textBox16
            // 
            this.textBox16.CanGrow = true;
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.200000762939453D), Telerik.Reporting.Drawing.Unit.Cm(1.1004006862640381D));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.9998018741607666D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox16.StyleName = "Caption";
            this.textBox16.Value = "Qtde. Programada";
            // 
            // textBox6
            // 
            this.textBox6.CanGrow = true;
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.500199317932129D), Telerik.Reporting.Drawing.Unit.Cm(0.600200355052948D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.2998003959655762D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox6.StyleName = "Data";
            this.textBox6.Value = "=Fields.Estilista";
            // 
            // textBox13
            // 
            this.textBox13.CanGrow = true;
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.200005531311035D), Telerik.Reporting.Drawing.Unit.Cm(1.1004006862640381D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.9499980211257935D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox13.StyleName = "Data";
            this.textBox13.Value = "=Fields.QuantidadeProgramada";
            // 
            // ListaModeloDataSource
            // 
            this.ListaModeloDataSource.DataSource = typeof(Fashion.ERP.Domain.EngenhariaProduto.Modelo);
            this.ListaModeloDataSource.Name = "ListaModeloDataSource";
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
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.5514583587646484D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.3999996185302734D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox12.StyleName = "PageInfo";
            this.textBox12.Value = "=NOW()";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.3999996185302734D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
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
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D), Telerik.Reporting.Drawing.Unit.Cm(0.049999367445707321D));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.3999996185302734D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "=\"Fashion ERP © \" + Now().Year";
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.5514583587646484D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.3999996185302734D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "= PageNumber + \'/\' + PageCount";
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
            this.titleTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.00010002159251598641D), Telerik.Reporting.Drawing.Unit.Cm(9.9921220680698752E-05D));
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(18.999797821044922D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.titleTextBox.StyleName = "Title";
            this.titleTextBox.Value = "Materiais da Programação de Produção";
            // 
            // reportFooter
            // 
            this.reportFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.64749175310134888D);
            this.reportFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.Filtros});
            this.reportFooter.Name = "reportFooter";
            // 
            // Filtros
            // 
            this.Filtros.CanGrow = true;
            this.Filtros.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.Filtros.Name = "Filtros";
            this.Filtros.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(18.897083282470703D), Telerik.Reporting.Drawing.Unit.Cm(0.59457510709762573D));
            this.Filtros.StyleName = "Caption";
            this.Filtros.Value = "= Parameters.Filtros.Value";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(2.5003006458282471D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.subReport1,
            this.textBox46});
            this.detail.KeepTogether = false;
            this.detail.Name = "detail";
            // 
            // subReport1
            // 
            this.subReport1.KeepTogether = false;
            this.subReport1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.60040056705474854D));
            this.subReport1.Name = "subReport1";
            instanceReportSource1.ReportDocument = this.materiaisProgramacaoProducaoSubReport1;
            this.subReport1.ReportSource = instanceReportSource1;
            this.subReport1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(18.94999885559082D), Telerik.Reporting.Drawing.Unit.Cm(1.8999001979827881D));
            // 
            // textBox46
            // 
            this.textBox46.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.textBox46.Name = "textBox46";
            this.textBox46.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(18.999898910522461D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox46.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox46.StyleName = "GroupPanel";
            this.textBox46.Value = "Materiais da Programação de Produção";
            // 
            // objectDataSourceMateriais
            // 
            this.objectDataSourceMateriais.Name = "objectDataSourceMateriais";
            // 
            // MateriaisProgramacaoProducaoReport
            // 
            this.DataSource = this.ListaModeloDataSource;
            this.ExternalStyleSheets.Add(new Telerik.Reporting.Drawing.ExternalStyleSheet("Fashion.ERP.Reporting.Resources.StyleSheet.xml"));
            group1.GroupFooter = this.groupFooterSection1;
            group1.GroupHeader = this.groupHeaderSection1;
            group1.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.Id"));
            group1.Name = "Grupo";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.groupHeaderSection1,
            this.groupFooterSection1,
            this.pageHeader,
            this.pageFooter,
            this.reportHeader,
            this.reportFooter,
            this.detail});
            this.Name = "MateriaisProgramacaoProducaoReport";
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(10D), Telerik.Reporting.Drawing.Unit.Mm(10D), Telerik.Reporting.Drawing.Unit.Mm(15D), Telerik.Reporting.Drawing.Unit.Mm(10D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AllowNull = true;
            reportParameter1.Name = "Filtros";
            this.ReportParameters.Add(reportParameter1);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(19D);
            ((System.ComponentModel.ISupportInitialize)(this.materiaisProgramacaoProducaoSubReport1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.ObjectDataSource ListaModeloDataSource;
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
        private Telerik.Reporting.GroupHeaderSection groupHeaderSection1;
        private Telerik.Reporting.GroupFooterSection groupFooterSection1;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox Filtros;
        private Telerik.Reporting.ObjectDataSource objectDataSourceMateriais;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox referenciaCaptionTextBox;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox referenciaDataTextBox;
        private Telerik.Reporting.TextBox descricaoDataTextBox;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.SubReport subReport1;
        private MateriaisProgramacaoProducaoSubReport materiaisProgramacaoProducaoSubReport1;
        private Telerik.Reporting.TextBox textBox46;

    }
}