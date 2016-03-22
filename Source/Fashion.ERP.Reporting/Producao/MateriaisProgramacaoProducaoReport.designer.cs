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
            Telerik.Reporting.TableGroup tableGroup1 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup2 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup3 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup4 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup5 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.TableGroup tableGroup6 = new Telerik.Reporting.TableGroup();
            Telerik.Reporting.InstanceReportSource instanceReportSource1 = new Telerik.Reporting.InstanceReportSource();
            Telerik.Reporting.Group group1 = new Telerik.Reporting.Group();
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            this.textBox31 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.materiaisProgramacaoProducaoSubReport1 = new Fashion.ERP.Reporting.Producao.MateriaisProgramacaoProducaoSubReport();
            this.groupFooterSection1 = new Telerik.Reporting.GroupFooterSection();
            this.groupHeaderSection1 = new Telerik.Reporting.GroupHeaderSection();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.table1 = new Telerik.Reporting.Table();
            this.textBox30 = new Telerik.Reporting.TextBox();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.textBox29 = new Telerik.Reporting.TextBox();
            this.textBox46 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
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
            this.objectDataSourceMateriais = new Telerik.Reporting.ObjectDataSource();
            ((System.ComponentModel.ISupportInitialize)(this.materiaisProgramacaoProducaoSubReport1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // textBox31
            // 
            this.textBox31.Name = "textBox31";
            this.textBox31.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.720994234085083D), Telerik.Reporting.Drawing.Unit.Cm(0.50000029802322388D));
            this.textBox31.StyleName = "Caption";
            this.textBox31.Value = "Referência";
            // 
            // textBox2
            // 
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.287992000579834D), Telerik.Reporting.Drawing.Unit.Cm(0.50000029802322388D));
            this.textBox2.StyleName = "Caption";
            this.textBox2.Value = "Descrição";
            // 
            // textBox1
            // 
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.0947051048278809D), Telerik.Reporting.Drawing.Unit.Cm(0.50000029802322388D));
            this.textBox1.StyleName = "Caption";
            this.textBox1.Value = "Estilista";
            // 
            // textBox3
            // 
            this.textBox3.Format = "{0}";
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.4063112735748291D), Telerik.Reporting.Drawing.Unit.Cm(0.49999982118606567D));
            this.textBox3.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox3.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0.20000000298023224D);
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8D);
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.StyleName = "";
            this.textBox3.Value = "=Fields.Tag";
            // 
            // materiaisProgramacaoProducaoSubReport1
            // 
            this.materiaisProgramacaoProducaoSubReport1.Name = "DetalheModeloReport";
            // 
            // groupFooterSection1
            // 
            this.groupFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.550000011920929D);
            this.groupFooterSection1.Name = "groupFooterSection1";
            // 
            // groupHeaderSection1
            // 
            this.groupHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(4.0001997947692871D);
            this.groupHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox8,
            this.table1,
            this.textBox29,
            this.textBox46,
            this.textBox18,
            this.textBox10,
            this.textBox9,
            this.textBox6,
            this.textBox16,
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
            // table1
            // 
            this.table1.Bindings.Add(new Telerik.Reporting.Binding("DataSource", "=ReportItem.DataObject.FichasTecnicas"));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(1.7209955453872681D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(7.287992000579834D)));
            this.table1.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Cm(6.0947046279907227D)));
            this.table1.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Cm(0.49999982118606567D)));
            this.table1.Body.SetCellContent(0, 0, this.textBox30);
            this.table1.Body.SetCellContent(0, 1, this.textBox17);
            this.table1.Body.SetCellContent(0, 2, this.textBox4);
            tableGroup1.Name = "Group2";
            tableGroup1.ReportItem = this.textBox31;
            tableGroup2.Name = "group1";
            tableGroup2.ReportItem = this.textBox2;
            tableGroup3.Name = "group2";
            tableGroup3.ReportItem = this.textBox1;
            this.table1.ColumnGroups.Add(tableGroup1);
            this.table1.ColumnGroups.Add(tableGroup2);
            this.table1.ColumnGroups.Add(tableGroup3);
            this.table1.Corner.SetCellContent(0, 0, this.textBox5);
            this.table1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox30,
            this.textBox17,
            this.textBox4,
            this.textBox31,
            this.textBox2,
            this.textBox1,
            this.textBox5,
            this.textBox3});
            this.table1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(2.0085418224334717D));
            this.table1.Name = "table1";
            tableGroup6.Name = "group";
            tableGroup5.ChildGroups.Add(tableGroup6);
            tableGroup5.Groupings.Add(new Telerik.Reporting.Grouping(null));
            tableGroup5.Name = "DetailGroup";
            tableGroup4.ChildGroups.Add(tableGroup5);
            tableGroup4.Name = "variacao1";
            tableGroup4.ReportItem = this.textBox3;
            this.table1.RowGroups.Add(tableGroup4);
            this.table1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(16.510002136230469D), Telerik.Reporting.Drawing.Unit.Cm(1.0000001192092896D));
            // 
            // textBox30
            // 
            this.textBox30.Name = "textBox30";
            this.textBox30.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.720994234085083D), Telerik.Reporting.Drawing.Unit.Cm(0.49999982118606567D));
            this.textBox30.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox30.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0.20000000298023224D);
            this.textBox30.StyleName = "Data";
            this.textBox30.Value = "= Fields.Referencia";
            // 
            // textBox17
            // 
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(7.287992000579834D), Telerik.Reporting.Drawing.Unit.Cm(0.49999982118606567D));
            this.textBox17.Style.BorderStyle.Bottom = Telerik.Reporting.Drawing.BorderType.None;
            this.textBox17.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0.20000000298023224D);
            this.textBox17.StyleName = "Data";
            this.textBox17.Value = "= Fields.Descricao";
            // 
            // textBox4
            // 
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.0947051048278809D), Telerik.Reporting.Drawing.Unit.Cm(0.49999982118606567D));
            this.textBox4.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(0.20000000298023224D);
            this.textBox4.StyleName = "Data";
            this.textBox4.Value = "= Fields.Estilista.Nome";
            // 
            // textBox5
            // 
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.4063112735748291D), Telerik.Reporting.Drawing.Unit.Cm(0.50000029802322388D));
            this.textBox5.StyleName = "Caption";
            this.textBox5.Value = "Tag";
            // 
            // textBox29
            // 
            this.textBox29.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(1.4000000953674316D));
            this.textBox29.Name = "textBox29";
            this.textBox29.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(16.547084808349609D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox29.StyleName = "GroupPanel";
            this.textBox29.Value = "Fichas Técnicas";
            // 
            // textBox46
            // 
            this.textBox46.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(3.4000999927520752D));
            this.textBox46.Name = "textBox46";
            this.textBox46.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(18.999898910522461D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox46.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Left;
            this.textBox46.StyleName = "GroupPanel";
            this.textBox46.Value = "Materiais da Programação de Produção";
            // 
            // textBox18
            // 
            this.textBox18.CanGrow = true;
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(13.255624771118164D), Telerik.Reporting.Drawing.Unit.Cm(0.600200355052948D));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.9998018741607666D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox18.StyleName = "Caption";
            this.textBox18.Value = "Qtde. Programada";
            // 
            // textBox10
            // 
            this.textBox10.CanGrow = true;
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(16.298332214355469D), Telerik.Reporting.Drawing.Unit.Cm(0.600200355052948D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.9499980211257935D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox10.StyleName = "Data";
            this.textBox10.Value = "=Fields.QuantidadeProgramada";
            // 
            // textBox9
            // 
            this.textBox9.CanGrow = true;
            this.textBox9.Format = "{0:d}";
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.1999998092651367D), Telerik.Reporting.Drawing.Unit.Cm(0.600200355052948D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.3925056457519531D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox9.StyleName = "Data";
            this.textBox9.Value = "=Fields.DataProgramada";
            // 
            // textBox6
            // 
            this.textBox6.CanGrow = true;
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.5616664886474609D), Telerik.Reporting.Drawing.Unit.Cm(0.600200355052948D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.6000006198883057D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox6.StyleName = "Caption";
            this.textBox6.Value = "Data Programada";
            // 
            // textBox16
            // 
            this.textBox16.CanGrow = true;
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.600200355052948D));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.5468839406967163D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox16.StyleName = "Caption";
            this.textBox16.Value = "Lote/Ano";
            // 
            // textBox13
            // 
            this.textBox13.CanGrow = true;
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.5610415935516357D), Telerik.Reporting.Drawing.Unit.Cm(0.600200355052948D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.9499980211257935D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox13.StyleName = "Data";
            this.textBox13.Value = "=Fields.Lote + \'/\' + Fields.Ano";
            // 
            // textBox15
            // 
            this.textBox15.CanGrow = true;
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.300199508666992D), Telerik.Reporting.Drawing.Unit.Cm(0.147391676902771D));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox15.StyleName = "Caption";
            this.textBox15.Value = "Qtd:";
            // 
            // textBox14
            // 
            this.textBox14.CanGrow = true;
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(17.900398254394531D), Telerik.Reporting.Drawing.Unit.Cm(0.147391676902771D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(0.85000139474868774D), Telerik.Reporting.Drawing.Unit.Cm(0.5D));
            this.textBox14.StyleName = "Caption";
            this.textBox14.Value = "= Count(Fields.Id)";
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
            this.Filtros,
            this.textBox15,
            this.textBox14});
            this.reportFooter.Name = "reportFooter";
            // 
            // Filtros
            // 
            this.Filtros.CanGrow = true;
            this.Filtros.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D), Telerik.Reporting.Drawing.Unit.Cm(0.052916664630174637D));
            this.Filtros.Name = "Filtros";
            this.Filtros.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(17.247081756591797D), Telerik.Reporting.Drawing.Unit.Cm(0.59457510709762573D));
            this.Filtros.StyleName = "Caption";
            this.Filtros.Value = "= Parameters.Filtros.Value";
            // 
            // detail
            // 
            this.detail.Height = Telerik.Reporting.Drawing.Unit.Cm(1.8999004364013672D);
            this.detail.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.subReport1});
            this.detail.KeepTogether = false;
            this.detail.Name = "detail";
            // 
            // subReport1
            // 
            this.subReport1.KeepTogether = false;
            this.subReport1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(3.5325686553733249E-07D));
            this.subReport1.Name = "subReport1";
            instanceReportSource1.ReportDocument = this.materiaisProgramacaoProducaoSubReport1;
            this.subReport1.ReportSource = instanceReportSource1;
            this.subReport1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(18.94999885559082D), Telerik.Reporting.Drawing.Unit.Cm(1.8999001979827881D));
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
        private Telerik.Reporting.SubReport subReport1;
        private MateriaisProgramacaoProducaoSubReport materiaisProgramacaoProducaoSubReport1;
        private Telerik.Reporting.TextBox textBox46;
        private Telerik.Reporting.Table table1;
        private Telerik.Reporting.TextBox textBox30;
        private Telerik.Reporting.TextBox textBox31;
        private Telerik.Reporting.TextBox textBox29;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox1;

    }
}