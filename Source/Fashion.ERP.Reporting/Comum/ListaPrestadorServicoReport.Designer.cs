namespace Fashion.ERP.Reporting.Comum
{
    partial class ListaPrestadorServicoReport
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group2 = new Telerik.Reporting.Group();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.nomeCaptionTextBox = new Telerik.Reporting.TextBox();
            this.tipoPessoaCaptionTextBox = new Telerik.Reporting.TextBox();
            this.cpfCnpjCaptionTextBox = new Telerik.Reporting.TextBox();
            this.PrestadorServicoDataSource = new Telerik.Reporting.ObjectDataSource();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
            this.titleTextBox = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.nomeDataTextBox = new Telerik.Reporting.TextBox();
            this.tipoPessoaDataTextBox = new Telerik.Reporting.TextBox();
            this.cpfCnpjDataTextBox = new Telerik.Reporting.TextBox();
            this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
            this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();
            this.Titulo = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.Quantidade = new Telerik.Reporting.TextBox();
            this.Filtros = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D);
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = false;
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.550000011920929D);
            this.labelsGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.nomeCaptionTextBox,
            this.tipoPessoaCaptionTextBox,
            this.cpfCnpjCaptionTextBox});
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.026458332315087318D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.9470834732055664D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "Código";
            // 
            // nomeCaptionTextBox
            // 
            this.nomeCaptionTextBox.CanGrow = true;
            this.nomeCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.0162498950958252D), Telerik.Reporting.Drawing.Unit.Cm(0.026458332315087318D));
            this.nomeCaptionTextBox.Name = "nomeCaptionTextBox";
            this.nomeCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.1995992660522461D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.nomeCaptionTextBox.StyleName = "Caption";
            this.nomeCaptionTextBox.Value = "Nome";
            // 
            // tipoPessoaCaptionTextBox
            // 
            this.tipoPessoaCaptionTextBox.CanGrow = true;
            this.tipoPessoaCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.223750114440918D), Telerik.Reporting.Drawing.Unit.Cm(0.026458332315087318D));
            this.tipoPessoaCaptionTextBox.Name = "tipoPessoaCaptionTextBox";
            this.tipoPessoaCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.8624026775360107D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.tipoPessoaCaptionTextBox.StyleName = "Caption";
            this.tipoPessoaCaptionTextBox.Value = "Tipo";
            // 
            // cpfCnpjCaptionTextBox
            // 
            this.cpfCnpjCaptionTextBox.CanGrow = true;
            this.cpfCnpjCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.086352348327637D), Telerik.Reporting.Drawing.Unit.Cm(0.026458332315087318D));
            this.cpfCnpjCaptionTextBox.Name = "cpfCnpjCaptionTextBox";
            this.cpfCnpjCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.8822927474975586D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.cpfCnpjCaptionTextBox.StyleName = "Caption";
            this.cpfCnpjCaptionTextBox.Value = "Cpf/Cnpj";
            // 
            // PrestadorServicoDataSource
            // 
            this.PrestadorServicoDataSource.DataSource = typeof(Fashion.ERP.Domain.Comum.Pessoa);
            this.PrestadorServicoDataSource.Name = "PrestadorServicoDataSource";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox3,
            this.textBox4});
            this.pageHeader.Name = "pageHeader";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D), Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.3999996185302734D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox3.StyleName = "PageInfo";
            this.textBox3.Value = "Fashion Consultoria & Sistemas";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.5514583587646484D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.3999996185302734D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox4.StyleName = "PageInfo";
            this.textBox4.Value = "=NOW()";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(1.1029165983200073D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageInfoTextBox,
            this.currentTimeTextBox,
            this.Filtros});
            this.pageFooter.Name = "pageFooter";
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.5514583587646484D), Telerik.Reporting.Drawing.Unit.Cm(0.60291659832000732D));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.3999996185302734D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "= PageNumber + \"/\" + PageCount";
            // 
            // currentTimeTextBox
            // 
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D), Telerik.Reporting.Drawing.Unit.Cm(0.59999990463256836D));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.3999996185302734D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "=\"Fashion ERP © \" + Now().Year";
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
            this.titleTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(18.897083282470703D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.titleTextBox.StyleName = "Title";
            this.titleTextBox.Value = "Prestadores de serviço";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.550000011920929D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox2,
            this.nomeDataTextBox,
            this.tipoPessoaDataTextBox,
            this.cpfCnpjDataTextBox});
            this.detail.Name = "detail";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.026458332315087318D), Telerik.Reporting.Drawing.Unit.Cm(0.026458332315087318D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.9470834732055664D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox2.StyleName = "Data";
            this.textBox2.Value = "=Fields.Cliente.Codigo";
            // 
            // nomeDataTextBox
            // 
            this.nomeDataTextBox.CanGrow = true;
            this.nomeDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.9897916316986084D), Telerik.Reporting.Drawing.Unit.Cm(0.026458332315087318D));
            this.nomeDataTextBox.Name = "nomeDataTextBox";
            this.nomeDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.1995992660522461D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.nomeDataTextBox.StyleName = "Data";
            this.nomeDataTextBox.Value = "=Fields.Nome";
            // 
            // tipoPessoaDataTextBox
            // 
            this.tipoPessoaDataTextBox.CanGrow = true;
            this.tipoPessoaDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.223750114440918D), Telerik.Reporting.Drawing.Unit.Cm(0.026458332315087318D));
            this.tipoPessoaDataTextBox.Name = "tipoPessoaDataTextBox";
            this.tipoPessoaDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.8624026775360107D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.tipoPessoaDataTextBox.StyleName = "Data";
            this.tipoPessoaDataTextBox.Value = "=Fields.TipoPessoa";
            // 
            // cpfCnpjDataTextBox
            // 
            this.cpfCnpjDataTextBox.CanGrow = true;
            this.cpfCnpjDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.107707977294922D), Telerik.Reporting.Drawing.Unit.Cm(0.026458332315087318D));
            this.cpfCnpjDataTextBox.Name = "cpfCnpjDataTextBox";
            this.cpfCnpjDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.8873958587646484D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.cpfCnpjDataTextBox.StyleName = "Data";
            this.cpfCnpjDataTextBox.Value = "=Fields.CpfCnpj";
            // 
            // groupHeaderSection1
            // 
            this.groupHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D);
            this.groupHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.Titulo});
            this.groupHeaderSection1.Name = "groupHeaderSection1";
            // 
            // groupFooterSection1
            // 
            this.groupFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.5D);
            this.groupFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox8,
            this.Quantidade});
            this.groupFooterSection1.Name = "groupFooterSection1";
            // 
            // Titulo
            // 
            this.Titulo.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.00020024616969749332D));
            this.Titulo.Name = "Titulo";
            this.Titulo.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(18.897083282470703D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.Titulo.StyleName = "GroupHeader";
            this.Titulo.Value = "";
            // 
            // textBox8
            // 
            this.textBox8.CanGrow = true;
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.300000190734863D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.6587638854980469D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox8.StyleName = "Caption";
            this.textBox8.Value = "Qtd Prestadores de serviço:";
            // 
            // Quantidade
            // 
            this.Quantidade.CanGrow = true;
            this.Quantidade.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.968645095825195D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.Quantidade.Name = "Quantidade";
            this.Quantidade.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.Quantidade.StyleName = "Caption";
            this.Quantidade.Value = "= Count(Fields.Id)";
            // 
            // Filtros
            // 
            this.Filtros.CanGrow = true;
            this.Filtros.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.Filtros.Name = "Filtros";
            this.Filtros.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(18.897083282470703D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.Filtros.StyleName = "Caption";
            this.Filtros.Value = "";
            // 
            // ListaPrestadorServicoReport
            // 
            this.DataSource = this.PrestadorServicoDataSource;
            this.ExternalStyleSheets.Add(new Telerik.Reporting.Drawing.ExternalStyleSheet("Fashion.ERP.Reporting.Resources.StyleSheet.xml"));
            group1.GroupFooter = this.labelsGroupFooterSection;
            group1.GroupHeader = this.labelsGroupHeaderSection;
            group1.Name = "labelsGroup";
            group2.GroupFooter = this.groupFooterSection1;
            group2.GroupHeader = this.groupHeaderSection1;
            group2.Name = "Grupo";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.pageHeader,
            this.pageFooter,
            this.reportHeader,
            this.detail,
            this.groupHeaderSection1,
            this.groupFooterSection1});
            this.Name = "ListaPrestadorServicoReport";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(10D), Telerik.Reporting.Drawing.Unit.Mm(10D), Telerik.Reporting.Drawing.Unit.Mm(15D), Telerik.Reporting.Drawing.Unit.Mm(20D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(19D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.ObjectDataSource PrestadorServicoDataSource;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.ReportHeaderSection reportHeader;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.TextBox titleTextBox;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox nomeCaptionTextBox;
        private Telerik.Reporting.TextBox tipoPessoaCaptionTextBox;
        private Telerik.Reporting.TextBox cpfCnpjCaptionTextBox;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox nomeDataTextBox;
        private Telerik.Reporting.TextBox tipoPessoaDataTextBox;
        private Telerik.Reporting.TextBox cpfCnpjDataTextBox;
        private Telerik.Reporting.GroupHeaderSection groupHeaderSection1;
        private Telerik.Reporting.GroupFooterSection groupFooterSection1;
        private Telerik.Reporting.TextBox Titulo;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox Quantidade;
        public Telerik.Reporting.TextBox Filtros;

    }
}