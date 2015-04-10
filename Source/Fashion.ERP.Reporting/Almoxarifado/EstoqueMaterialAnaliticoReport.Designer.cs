namespace Fashion.ERP.Reporting.Almoxarifado
{
    partial class EstoqueMaterialAnaliticoReport
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.Reporting.Drawing.FormattingRule formattingRule1 = new Telerik.Reporting.Drawing.FormattingRule();
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.Group group2 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.titleTextBox = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.groupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.groupHeader = new Telerik.Reporting.GroupHeaderSection();
            this.Titulo = new Telerik.Reporting.TextBox();
            this.referenciaCaptionTextBox = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.EstoqueMaterialSinteticoModelDataSource = new Telerik.Reporting.ObjectDataSource();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
            this.reportFooter = new Telerik.Reporting.ReportFooterSection();
            this.Filtros = new Telerik.Reporting.TextBox();
            this.detail = new Telerik.Reporting.DetailSection();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.referenciaDataTextBox = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.60271662473678589D);
            this.labelsGroupFooterSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox15,
            this.textBox14});
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            this.labelsGroupFooterSection.Style.Visible = false;
            // 
            // textBox15
            // 
            this.textBox15.CanGrow = true;
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(18D), Telerik.Reporting.Drawing.Unit.Cm(0.049899648874998093D));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.89979809522628784D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox15.StyleName = "Caption";
            this.textBox15.Value = "Qtd:";
            // 
            // textBox14
            // 
            this.textBox14.CanGrow = true;
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(18.899999618530273D), Telerik.Reporting.Drawing.Unit.Cm(0.049899648874998093D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.95541971921920776D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox14.StyleName = "Caption";
            this.textBox14.Value = "= Count(Fields.Id)";
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(1.0677081346511841D);
            this.labelsGroupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.titleTextBox,
            this.textBox19});
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.050000175833702087D), Telerik.Reporting.Drawing.Unit.Cm(9.9719363788608462E-05D));
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.494279861450195D), Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D));
            this.titleTextBox.StyleName = "Title";
            this.titleTextBox.Value = "Estoque de Material";
            // 
            // textBox19
            // 
            this.textBox19.CanGrow = true;
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.544480323791504D), Telerik.Reporting.Drawing.Unit.Cm(0.40009972453117371D));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.4554190635681152D), Telerik.Reporting.Drawing.Unit.Cm(0.59999996423721313D));
            this.textBox19.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox19.StyleName = "Caption";
            this.textBox19.Value = "Sintético";
            // 
            // groupFooterSection
            // 
            this.groupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.18199111521244049D);
            this.groupFooterSection.Name = "groupFooterSection";
            // 
            // groupHeader
            // 
            this.groupHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(0.60019999742507935D);
            this.groupHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.Titulo});
            this.groupHeader.Name = "groupHeader";
            // 
            // Titulo
            // 
            this.Titulo.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.050000175833702087D), Telerik.Reporting.Drawing.Unit.Cm(0.00019984244136139751D));
            this.Titulo.Name = "Titulo";
            this.Titulo.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(19.94999885559082D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.Titulo.StyleName = "GroupHeader";
            this.Titulo.Value = "";
            // 
            // referenciaCaptionTextBox
            // 
            this.referenciaCaptionTextBox.CanGrow = true;
            this.referenciaCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.3999948501586914D), Telerik.Reporting.Drawing.Unit.Cm(0.39980015158653259D));
            this.referenciaCaptionTextBox.Name = "referenciaCaptionTextBox";
            this.referenciaCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.4000006914138794D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.referenciaCaptionTextBox.StyleName = "Caption";
            this.referenciaCaptionTextBox.Value = "Unidade";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.3999948501586914D), Telerik.Reporting.Drawing.Unit.Cm(1.1173083782196045D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.4000011682510376D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "Depósito";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.5999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0.39980036020278931D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.4997998476028442D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox2.StyleName = "Caption";
            this.textBox2.Value = "Referência";
            // 
            // textBox3
            // 
            this.textBox3.CanGrow = true;
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.5999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(1.1173082590103149D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.5000007152557373D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox3.StyleName = "Caption";
            this.textBox3.Value = "Descrição";
            // 
            // textBox16
            // 
            this.textBox16.CanGrow = true;
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.912327766418457D), Telerik.Reporting.Drawing.Unit.Cm(1.7998005151748657D));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.2698309421539307D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox16.StyleName = "Caption";
            this.textBox16.Value = "Qtde.Reservada";
            // 
            // textBox18
            // 
            this.textBox18.CanGrow = true;
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.912327766418457D), Telerik.Reporting.Drawing.Unit.Cm(2.4997997283935547D));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.2698311805725098D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox18.StyleName = "Caption";
            this.textBox18.Value = "Qtde.Disponível";
            // 
            // textBox5
            // 
            this.textBox5.CanGrow = true;
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.912327766418457D), Telerik.Reporting.Drawing.Unit.Cm(1.1173082590103149D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.2698309421539307D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox5.StyleName = "Caption";
            this.textBox5.Value = "Qtde.Estoque";
            // 
            // textBox4
            // 
            this.textBox4.CanGrow = true;
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.912327766418457D), Telerik.Reporting.Drawing.Unit.Cm(0.39980036020278931D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.5176435708999634D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox4.StyleName = "Caption";
            this.textBox4.Value = "Und.";
            // 
            // EstoqueMaterialSinteticoModelDataSource
            // 
            this.EstoqueMaterialSinteticoModelDataSource.DataSource = typeof(Fashion.ERP.Reporting.Almoxarifado.Models.EstoqueMaterialSinteticoModel);
            this.EstoqueMaterialSinteticoModelDataSource.Name = "EstoqueMaterialSinteticoModelDataSource";
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
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.800200462341309D), Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.1996994018554688D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox12.StyleName = "PageInfo";
            this.textBox12.Value = "=NOW()";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D), Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(13.75D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
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
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(10.649999618530273D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "=\"Fashion ERP © \" + Now().Year";
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.9998989105224609D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "= PageNumber + \'/\' + PageCount";
            // 
            // reportHeader
            // 
            this.reportHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(0.13229165971279144D);
            this.reportHeader.Name = "reportHeader";
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
            this.Filtros.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(19.949798583984375D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.Filtros.StyleName = "Caption";
            this.Filtros.Value = "= Parameters.Filtros.Value";
            // 
            // detail
            // 
            formattingRule1.Filters.Add(new Telerik.Reporting.Filter("= RowNumber()%2", Telerik.Reporting.FilterOperator.Equal, "1"));
            formattingRule1.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.detail.ConditionalFormatting.AddRange(new Telerik.Reporting.Drawing.FormattingRule[] {
            formattingRule1});
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(3.2998003959655762D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox10,
            this.textBox8,
            this.textBox7,
            this.textBox13,
            this.textBox17,
            this.textBox6,
            this.referenciaDataTextBox,
            this.textBox9,
            this.textBox4,
            this.textBox5,
            this.textBox16,
            this.textBox18,
            this.referenciaCaptionTextBox,
            this.textBox1,
            this.textBox2,
            this.textBox3,
            this.pictureBox1,
            this.textBox20,
            this.textBox21,
            this.textBox23,
            this.textBox22,
            this.textBox24,
            this.textBox25,
            this.textBox26,
            this.textBox27});
            this.detail.Name = "detail";
            // 
            // textBox10
            // 
            this.textBox10.CanGrow = true;
            this.textBox10.Format = "{0:#.0000}";
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.18235969543457D), Telerik.Reporting.Drawing.Unit.Cm(1.1173082590103149D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5178360939025879D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox10.StyleName = "Data";
            this.textBox10.Value = "=Fields.QuantidadeEstoque";
            // 
            // textBox8
            // 
            this.textBox8.CanGrow = true;
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.099998950958252D), Telerik.Reporting.Drawing.Unit.Cm(1.1173082590103149D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.100196361541748D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox8.StyleName = "Data";
            this.textBox8.Value = "=Fields.Descricao";
            // 
            // textBox7
            // 
            this.textBox7.CanGrow = true;
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.099998950958252D), Telerik.Reporting.Drawing.Unit.Cm(0.39980036020278931D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.10019588470459D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox7.StyleName = "Data";
            this.textBox7.Value = "=Fields.Referencia";
            // 
            // textBox13
            // 
            this.textBox13.CanGrow = true;
            this.textBox13.Format = "{0:#.0000}";
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.18235969543457D), Telerik.Reporting.Drawing.Unit.Cm(1.7998005151748657D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5178358554840088D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox13.StyleName = "Data";
            this.textBox13.Value = "=Fields.QuantidadeReservada";
            // 
            // textBox17
            // 
            this.textBox17.CanGrow = true;
            this.textBox17.Format = "{0:#.0000}";
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.18235969543457D), Telerik.Reporting.Drawing.Unit.Cm(2.4997992515563965D));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5178358554840088D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox17.StyleName = "Data";
            this.textBox17.Value = "=Fields.QuantidadeDisponivel";
            // 
            // textBox6
            // 
            this.textBox6.CanGrow = true;
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.800195693969727D), Telerik.Reporting.Drawing.Unit.Cm(1.1173082590103149D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.7997994422912598D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox6.StyleName = "Data";
            this.textBox6.Value = "=Fields.DepositoMaterial";
            // 
            // referenciaDataTextBox
            // 
            this.referenciaDataTextBox.CanGrow = true;
            this.referenciaDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.800195693969727D), Telerik.Reporting.Drawing.Unit.Cm(0.39979994297027588D));
            this.referenciaDataTextBox.Name = "referenciaDataTextBox";
            this.referenciaDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.7998008728027344D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.referenciaDataTextBox.StyleName = "Data";
            this.referenciaDataTextBox.Value = "=Fields.Unidade";
            // 
            // textBox9
            // 
            this.textBox9.CanGrow = true;
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.18235969543457D), Telerik.Reporting.Drawing.Unit.Cm(0.39980036020278931D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5178358554840088D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox9.StyleName = "Data";
            this.textBox9.Value = "=Fields.UnidadeMedida";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Bindings.Add(new Telerik.Reporting.Binding("Value", "= FashionErp.EnderecoFoto(Fields.Foto)"));
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2999999523162842D), Telerik.Reporting.Drawing.Unit.Cm(3.2997000217437744D));
            this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
            // 
            // textBox20
            // 
            this.textBox20.CanGrow = true;
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.5997991561889648D), Telerik.Reporting.Drawing.Unit.Cm(1.7998005151748657D));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.5000007152557373D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox20.StyleName = "Caption";
            this.textBox20.Value = "Categoria";
            // 
            // textBox21
            // 
            this.textBox21.CanGrow = true;
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.099998950958252D), Telerik.Reporting.Drawing.Unit.Cm(1.7998005151748657D));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.1001954078674316D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox21.StyleName = "Data";
            this.textBox21.Value = "=Fields.Categoria";
            // 
            // textBox23
            // 
            this.textBox23.CanGrow = true;
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.5081243515014648D), Telerik.Reporting.Drawing.Unit.Cm(2.4998002052307129D));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.6920702457427979D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox23.StyleName = "Data";
            this.textBox23.Value = "=Fields.Subcategoria";
            // 
            // textBox22
            // 
            this.textBox22.CanGrow = true;
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.5997991561889648D), Telerik.Reporting.Drawing.Unit.Cm(2.499800443649292D));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.9081251621246338D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox22.StyleName = "Caption";
            this.textBox22.Value = "Subcategoria";
            // 
            // textBox24
            // 
            this.textBox24.CanGrow = true;
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.3999948501586914D), Telerik.Reporting.Drawing.Unit.Cm(1.7998005151748657D));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.4000011682510376D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox24.StyleName = "Caption";
            this.textBox24.Value = "Familia";
            // 
            // textBox25
            // 
            this.textBox25.CanGrow = true;
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.800195693969727D), Telerik.Reporting.Drawing.Unit.Cm(1.7998005151748657D));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.7997994422912598D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox25.StyleName = "Data";
            this.textBox25.Value = "=Fields.Familia";
            // 
            // textBox26
            // 
            this.textBox26.CanGrow = true;
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.3999948501586914D), Telerik.Reporting.Drawing.Unit.Cm(2.4997992515563965D));
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.4000011682510376D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox26.StyleName = "Caption";
            this.textBox26.Value = "Marca";
            // 
            // textBox27
            // 
            this.textBox27.CanGrow = true;
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.800195693969727D), Telerik.Reporting.Drawing.Unit.Cm(2.4997992515563965D));
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.7997994422912598D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox27.StyleName = "Data";
            this.textBox27.Value = "=Fields.Marca";
            // 
            // EstoqueMaterialAnaliticoReport
            // 
            this.DataSource = this.EstoqueMaterialSinteticoModelDataSource;
            this.ExternalStyleSheets.Add(new Telerik.Reporting.Drawing.ExternalStyleSheet("Fashion.ERP.Reporting.Resources.StyleSheet.xml"));
            group1.GroupFooter = this.labelsGroupFooterSection;
            group1.GroupHeader = this.labelsGroupHeaderSection;
            group1.Name = "labelsGroup";
            group2.GroupFooter = this.groupFooterSection;
            group2.GroupHeader = this.groupHeader;
            group2.Name = "Grupo";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.groupHeader,
            this.groupFooterSection,
            this.pageHeader,
            this.pageFooter,
            this.reportHeader,
            this.reportFooter,
            this.detail});
            this.Name = "EstoqueMaterialSinteticoReport";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(5D), Telerik.Reporting.Drawing.Unit.Mm(5D), Telerik.Reporting.Drawing.Unit.Mm(10D), Telerik.Reporting.Drawing.Unit.Mm(5D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AllowNull = true;
            reportParameter1.Name = "Filtros";
            this.ReportParameters.Add(reportParameter1);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(19.999898910522461D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.ObjectDataSource EstoqueMaterialSinteticoModelDataSource;
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
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.GroupHeaderSection groupHeader;
        private Telerik.Reporting.GroupFooterSection groupFooterSection;
        private Telerik.Reporting.TextBox Titulo;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox27;

    }
}