namespace Fashion.ERP.Reporting.EngenhariaProduto
{
    partial class ListagemModelosAprovadosReport
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
            Telerik.Reporting.Group group3 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            this.labelsGroupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.Quantidade = new Telerik.Reporting.TextBox();
            this.labelsGroupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.groupFooterSection = new Telerik.Reporting.GroupFooterSection();
            this.groupHeaderSection = new Telerik.Reporting.GroupHeaderSection();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.referenciaCaptionTextBox = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.tecidoCaptionTextBox = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.ListagemModelosAprovadosDataSource = new Telerik.Reporting.ObjectDataSource();
            this.pageHeader = new Telerik.Reporting.PageHeaderSection();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.pageFooter = new Telerik.Reporting.PageFooterSection();
            this.pageInfoTextBox = new Telerik.Reporting.TextBox();
            this.currentTimeTextBox = new Telerik.Reporting.TextBox();
            this.reportFooter = new Telerik.Reporting.ReportFooterSection();
            this.detail = new Telerik.Reporting.DetailSection();
            this.tecidoDataTextBox = new Telerik.Reporting.TextBox();
            this.descricaoDataTextBox = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.reportHeader = new Telerik.Reporting.ReportHeaderSection();
            this.titleTextBox = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // labelsGroupFooterSection
            // 
            this.labelsGroupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.5D);
            this.labelsGroupFooterSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox10,
            this.textBox9,
            this.textBox13,
            this.Quantidade});
            this.labelsGroupFooterSection.Name = "labelsGroupFooterSection";
            // 
            // textBox10
            // 
            this.textBox10.CanGrow = true;
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.1000001430511475D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox10.StyleName = "Caption";
            this.textBox10.Value = "Total modelos:";
            // 
            // textBox9
            // 
            this.textBox9.CanGrow = true;
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.1000001430511475D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.85000139474868774D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox9.StyleName = "Caption";
            this.textBox9.Value = "= Count(Fields.Id)";
            // 
            // textBox13
            // 
            this.textBox13.CanGrow = true;
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.200000762939453D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.4462504386901856D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox13.StyleName = "Caption";
            this.textBox13.Value = "Qtd Total:";
            // 
            // Quantidade
            // 
            this.Quantidade.CanGrow = true;
            this.Quantidade.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.694385528564453D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.Quantidade.Name = "Quantidade";
            this.Quantidade.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.3055148124694824D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.Quantidade.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.Quantidade.StyleName = "Caption";
            this.Quantidade.Value = "= Sum(Fields.QuantidadeProducao)";
            // 
            // labelsGroupHeaderSection
            // 
            this.labelsGroupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.13229167461395264D);
            this.labelsGroupHeaderSection.Name = "labelsGroupHeaderSection";
            this.labelsGroupHeaderSection.PrintOnEveryPage = true;
            // 
            // groupFooterSection
            // 
            this.groupFooterSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.13229167461395264D);
            this.groupFooterSection.Name = "groupFooterSection";
            // 
            // groupHeaderSection
            // 
            this.groupHeaderSection.Height = Telerik.Reporting.Drawing.Unit.Cm(0.56780833005905151D);
            this.groupHeaderSection.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox6,
            this.textBox8,
            this.textBox16,
            this.textBox3,
            this.referenciaCaptionTextBox,
            this.textBox17});
            this.groupHeaderSection.Name = "groupHeaderSection";
            this.groupHeaderSection.Style.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            // 
            // textBox6
            // 
            this.textBox6.CanGrow = true;
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.3001997470855713D), Telerik.Reporting.Drawing.Unit.Cm(0.047183003276586533D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.0998003482818604D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox6.StyleName = "Data";
            this.textBox6.Value = "=Fields.Modelo.Referencia";
            // 
            // textBox8
            // 
            this.textBox8.CanGrow = true;
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.0497989654541016D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(5.2442798614501953D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox8.StyleName = "Data";
            this.textBox8.TextWrap = true;
            this.textBox8.Value = "=Fields.Modelo.Tecido";
            // 
            // textBox16
            // 
            this.textBox16.CanGrow = true;
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.294279098510742D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.90552175045013428D), Telerik.Reporting.Drawing.Unit.Cm(0.49999997019767761D));
            this.textBox16.StyleName = "Caption";
            this.textBox16.Value = "Forro:";
            // 
            // textBox3
            // 
            this.textBox3.CanGrow = true;
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.0495986938476562D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.99999970197677612D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox3.StyleName = "Caption";
            this.textBox3.Value = "Tecido:";
            // 
            // referenciaCaptionTextBox
            // 
            this.referenciaCaptionTextBox.CanGrow = true;
            this.referenciaCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.026458332315087318D), Telerik.Reporting.Drawing.Unit.Cm(0.047183003276586533D));
            this.referenciaCaptionTextBox.Name = "referenciaCaptionTextBox";
            this.referenciaCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2735414505004883D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.referenciaCaptionTextBox.StyleName = "Caption";
            this.referenciaCaptionTextBox.Value = "Referência do Modelo:";
            // 
            // textBox17
            // 
            this.textBox17.CanGrow = true;
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(15.200000762939453D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.7998981475830078D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox17.StyleName = "Data";
            this.textBox17.Value = "=Fields.Modelo.Forro";
            // 
            // groupFooterSection1
            // 
            this.groupFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(1.4529165029525757D);
            this.groupFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox22,
            this.textBox19,
            this.tecidoCaptionTextBox,
            this.textBox4,
            this.textBox5});
            this.groupFooterSection1.Name = "groupFooterSection1";
            this.groupFooterSection1.Style.BorderColor.Bottom = System.Drawing.Color.Silver;
            this.groupFooterSection1.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Groove;
            // 
            // textBox22
            // 
            this.textBox22.CanGrow = true;
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.00020064989803358913D));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.2470831871032715D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox22.StyleName = "Caption";
            this.textBox22.Value = "Observação do modelo:";
            // 
            // textBox19
            // 
            this.textBox19.CanGrow = true;
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.69438362121582D), Telerik.Reporting.Drawing.Unit.Cm(0.00020064989803358913D));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.2806148529052734D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox19.StyleName = "Caption";
            this.textBox19.Value = "= Sum(Fields.QuantidadeProducao)";
            // 
            // tecidoCaptionTextBox
            // 
            this.tecidoCaptionTextBox.CanGrow = true;
            this.tecidoCaptionTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(14.86472225189209D), Telerik.Reporting.Drawing.Unit.Cm(0.026458332315087318D));
            this.tecidoCaptionTextBox.Name = "tecidoCaptionTextBox";
            this.tecidoCaptionTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.8294622898101807D), Telerik.Reporting.Drawing.Unit.Cm(0.52625763416290283D));
            this.tecidoCaptionTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.tecidoCaptionTextBox.StyleName = "Caption";
            this.tecidoCaptionTextBox.Value = "Total:";
            // 
            // textBox4
            // 
            this.textBox4.CanGrow = true;
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.3001997470855713D), Telerik.Reporting.Drawing.Unit.Cm(0.026458535343408585D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.9952383041381836D), Telerik.Reporting.Drawing.Unit.Cm(0.92635798454284668D));
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox4.StyleName = "Data";
            this.textBox4.Value = "=Fields.Modelo.Observacao";
            // 
            // textBox5
            // 
            this.textBox5.CanGrow = true;
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(3.3001997470855713D), Telerik.Reporting.Drawing.Unit.Cm(0.95301675796508789D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(15.699698448181152D), Telerik.Reporting.Drawing.Unit.Cm(0.49989986419677734D));
            this.textBox5.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox5.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox5.Style.BorderStyle.Left = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox5.Style.BorderStyle.Right = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox5.Style.BorderStyle.Top = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox5.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox5.Style.BorderWidth.Top = Telerik.Reporting.Drawing.Unit.Point(0.5D);
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Top;
            this.textBox5.StyleName = "Data";
            this.textBox5.Value = "";
            // 
            // groupHeaderSection1
            // 
            this.groupHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.54708296060562134D);
            this.groupHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox1,
            this.textBox2,
            this.textBox20,
            this.textBox21});
            this.groupHeaderSection1.Name = "groupHeaderSection1";
            // 
            // textBox1
            // 
            this.textBox1.CanGrow = true;
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.99999988079071045D), Telerik.Reporting.Drawing.Unit.Cm(0.046982757747173309D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.4500000476837158D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "Tag";
            // 
            // textBox2
            // 
            this.textBox2.CanGrow = true;
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.450200080871582D), Telerik.Reporting.Drawing.Unit.Cm(0.00010012308484874666D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.4495992660522461D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox2.StyleName = "Caption";
            this.textBox2.Value = "Descrição";
            // 
            // textBox20
            // 
            this.textBox20.CanGrow = true;
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.899999618530273D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.7941837310791016D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox20.StyleName = "Caption";
            this.textBox20.Value = "Dificuldade";
            // 
            // textBox21
            // 
            this.textBox21.CanGrow = true;
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.69438362121582D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.3055152893066406D), Telerik.Reporting.Drawing.Unit.Cm(0.55291664600372314D));
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox21.StyleName = "Caption";
            this.textBox21.Value = "Quantidade";
            // 
            // ListagemModelosAprovadosDataSource
            // 
            this.ListagemModelosAprovadosDataSource.DataSource = "Fashion.ERP.Domain.EngenhariaProduto.Modelo, Fashion.ERP.Domain, Version=0.1.30.0" +
    ", Culture=neutral, PublicKeyToken=null";
            this.ListagemModelosAprovadosDataSource.Name = "ListagemModelosAprovadosDataSource";
            // 
            // pageHeader
            // 
            this.pageHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D);
            this.pageHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox11,
            this.textBox12});
            this.pageHeader.Name = "pageHeader";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.3999996185302734D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox11.StyleName = "PageInfo";
            this.textBox11.Value = "Fashion Consultoria & Sistemas";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.5514583587646484D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.4484405517578125D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox12.StyleName = "PageInfo";
            this.textBox12.Value = "=NOW()";
            // 
            // pageFooter
            // 
            this.pageFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D);
            this.pageFooter.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageInfoTextBox,
            this.currentTimeTextBox});
            this.pageFooter.Name = "pageFooter";
            // 
            // pageInfoTextBox
            // 
            this.pageInfoTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.5514583587646484D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.pageInfoTextBox.Name = "pageInfoTextBox";
            this.pageInfoTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.4484415054321289D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.pageInfoTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.pageInfoTextBox.StyleName = "PageInfo";
            this.pageInfoTextBox.Value = "= PageNumber";
            // 
            // currentTimeTextBox
            // 
            this.currentTimeTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.05000000074505806D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.currentTimeTextBox.Name = "currentTimeTextBox";
            this.currentTimeTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.3999996185302734D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.currentTimeTextBox.StyleName = "PageInfo";
            this.currentTimeTextBox.Value = "=\"Fashion ERP © \" + Now().Year";
            // 
            // reportFooter
            // 
            this.reportFooter.Height = Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D);
            this.reportFooter.Name = "reportFooter";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(0.50000005960464478D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.tecidoDataTextBox,
            this.descricaoDataTextBox,
            this.textBox18,
            this.textBox7});
            this.detail.Name = "detail";
            // 
            // tecidoDataTextBox
            // 
            this.tecidoDataTextBox.CanGrow = true;
            this.tecidoDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.69438362121582D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.tecidoDataTextBox.Name = "tecidoDataTextBox";
            this.tecidoDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.3055152893066406D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.tecidoDataTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.tecidoDataTextBox.StyleName = "Data";
            this.tecidoDataTextBox.Value = "=Fields.QuantidadeProducao";
            // 
            // descricaoDataTextBox
            // 
            this.descricaoDataTextBox.CanGrow = true;
            this.descricaoDataTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(2.450200080871582D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.descricaoDataTextBox.Name = "descricaoDataTextBox";
            this.descricaoDataTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(9.4495992660522461D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.descricaoDataTextBox.StyleName = "Data";
            this.descricaoDataTextBox.Value = "=Fields.Descricao";
            // 
            // textBox18
            // 
            this.textBox18.CanGrow = true;
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.97665667533874512D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.4733430147171021D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox18.StyleName = "Data";
            this.textBox18.Value = "=Fields.Referencia";
            // 
            // textBox7
            // 
            this.textBox7.CanGrow = true;
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(11.90000057220459D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(4.7941837310791016D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox7.StyleName = "Data";
            this.textBox7.Value = "=Fields.classificacaodificuldade.Descricao";
            // 
            // reportHeader
            // 
            this.reportHeader.Height = Telerik.Reporting.Drawing.Unit.Cm(0.99990010261535645D);
            this.reportHeader.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.titleTextBox,
            this.textBox14});
            this.reportHeader.Name = "reportHeader";
            this.reportHeader.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.Groove;
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(9.9921220680698752E-05D));
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(10.84708309173584D), Telerik.Reporting.Drawing.Unit.Cm(0.99980014562606812D));
            this.titleTextBox.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.titleTextBox.StyleName = "Title";
            this.titleTextBox.Value = "Listagem de modelos aprovados";
            // 
            // textBox14
            // 
            this.textBox14.CanGrow = true;
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.900199890136719D), Telerik.Reporting.Drawing.Unit.Cm(0.40449565649032593D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.0996990203857422D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox14.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox14.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10D);
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.textBox14.StyleName = "Caption";
            this.textBox14.Value = "= Parameters.Filtros.Value";
            // 
            // ListagemModelosAprovadosReport
            // 
            this.DataSource = this.ListagemModelosAprovadosDataSource;
            this.ExternalStyleSheets.Add(new Telerik.Reporting.Drawing.ExternalStyleSheet("Fashion.ERP.Reporting.Resources.StyleSheet.xml"));
            group1.GroupFooter = this.labelsGroupFooterSection;
            group1.GroupHeader = this.labelsGroupHeaderSection;
            group1.Name = "labelsGroup";
            group2.GroupFooter = this.groupFooterSection;
            group2.GroupHeader = this.groupHeaderSection;
            group2.Groupings.Add(new Telerik.Reporting.Grouping("=Fields.Modelo.Referencia"));
            group2.Name = "Grupo";
            group3.GroupFooter = this.groupFooterSection1;
            group3.GroupHeader = this.groupHeaderSection1;
            group3.Name = "group";
            this.Groups.AddRange(new Telerik.Reporting.Group[] {
            group1,
            group2,
            group3});
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.labelsGroupHeaderSection,
            this.labelsGroupFooterSection,
            this.groupHeaderSection,
            this.groupFooterSection,
            this.groupHeaderSection1,
            this.groupFooterSection1,
            this.pageHeader,
            this.pageFooter,
            this.reportHeader,
            this.reportFooter,
            this.detail});
            this.Name = "ListagemModelosAprovados";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(10D), Telerik.Reporting.Drawing.Unit.Mm(10D), Telerik.Reporting.Drawing.Unit.Mm(15D), Telerik.Reporting.Drawing.Unit.Mm(10D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.AllowNull = true;
            reportParameter1.Name = "Filtros";
            this.ReportParameters.Add(reportParameter1);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(18.999898910522461D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.ObjectDataSource ListagemModelosAprovadosDataSource;
        private Telerik.Reporting.GroupHeaderSection labelsGroupHeaderSection;
        private Telerik.Reporting.GroupFooterSection labelsGroupFooterSection;
        private Telerik.Reporting.PageHeaderSection pageHeader;
        private Telerik.Reporting.PageFooterSection pageFooter;
        private Telerik.Reporting.ReportFooterSection reportFooter;
        private Telerik.Reporting.DetailSection detail;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.ReportHeaderSection reportHeader;
        private Telerik.Reporting.TextBox titleTextBox;
        private Telerik.Reporting.GroupHeaderSection groupHeaderSection;
        private Telerik.Reporting.GroupFooterSection groupFooterSection;
        private Telerik.Reporting.TextBox pageInfoTextBox;
        private Telerik.Reporting.TextBox currentTimeTextBox;
        private Telerik.Reporting.TextBox tecidoCaptionTextBox;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox referenciaCaptionTextBox;
        private Telerik.Reporting.TextBox tecidoDataTextBox;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox descricaoDataTextBox;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.GroupHeaderSection groupHeaderSection1;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.GroupFooterSection groupFooterSection1;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox Quantidade;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox5;

    }
}