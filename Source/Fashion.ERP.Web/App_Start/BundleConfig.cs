using System.Web.Optimization;

namespace Links
{
    public static partial class Bundles
    {
        #region Styles
        public static partial class Styles
        {
            public static readonly string Layout = "~/Content/layout";
            public static readonly string Flatdream = "~/Content/flatdream";
            public static readonly string Bootstrap = "~/Content/bootstrap-img";
            public static readonly string Kendo = "~/Content/kendo";
            public static readonly string Login = "~/Content/login";
            public static readonly string JasnyBootstrap = "~/Content/jasnybootstrap";
            public static readonly string FotoUpload = "~/Content/fotoupload";
            public static readonly string Lightbox = "~/Content/lightbox-img";
            public static readonly string JqueryPushMenu = "~/Content/jpushmenu";
            public static readonly string JqueryNanoScroller = "~/Content/jquerynanoscroller";
            public static readonly string FontAwesome = "~/Content/font-awesome";
            public static readonly string Site = "~/Content/site";
        }
        #endregion

        #region Scripts
        public static partial class Scripts
        {
            public static readonly string Flatdream = "~/bundles/flatdream";
            public static readonly string Json2 = "~/bundles/json2";
            public static readonly string Jquery = "~/bundles/jquery";
            public static readonly string JqueryValidation = "~/bundles/jqueryval";
            public static readonly string JqueryPushMenu = "~/bundles/jpushmenu";
            public static readonly string JqueryNanoScroller = "~/bundles/jquerynanoscroller";
            public static readonly string JqueryCookie= "~/bundles/jquerycookie";
            public static readonly string JqueryGlobalize = "~/bundles/globalize";
            public static readonly string Bootstrap = "~/bundles/bootstrap";
            public static readonly string Modernizr = "~/bundles/modernizr";
            public static readonly string Kendo = "~/bundles/kendoui";
            public static readonly string FashionErp = "~/bundles/fashion";
            public static readonly string JasnyBootstrap = "~/bundles/jasnybootstrap";
            public static readonly string JqueryForm = "~/bundles/jform";
            public static readonly string ArquivoUpload = "~/bundles/arquivoupload";
            public static readonly string FotoUpload = "~/bundles/fotoupload";
            public static readonly string JqueryColor = "~/bundles/jcolor";
            public static readonly string PdfObject = "~/bundles/pdfobject";
            public static readonly string Lightbox = "~/bundles/lightbox";
        }
        #endregion
    }
}

namespace Fashion.ERP.Web
{
    public class BundleConfig
    {
        #region RegisterBundles
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            /////////////////// Styles ////////////////////////

            bundles.Add(new StyleBundle(Links.Bundles.Styles.Flatdream)
                .Include("~/Content/style.css",
                    "~/Content/pygments.css"));

            bundles.Add(new StyleBundle(Links.Bundles.Styles.Layout)
                .Include("~/Content/layout.css"));

            bundles.Add(new StyleBundle(Links.Bundles.Styles.Login)
                .Include("~/Content/login.css"));

            bundles.Add(new StyleBundle(Links.Bundles.Styles.Bootstrap)
                .Include("~/Content/bootstrap.css",
                    "~/Content/bootstrap.css.map"));

            bundles.Add(new StyleBundle(Links.Bundles.Styles.Kendo)
                .Include("~/Content/kendo.common.css",
                    "~/Content/kendo.bootstrap.css"));

            bundles.Add(new StyleBundle(Links.Bundles.Styles.JasnyBootstrap)
                .Include("~/Content/jasny-bootstrap.css"));

            bundles.Add(new StyleBundle(Links.Bundles.Styles.FotoUpload)
                .Include("~/Content/jquery.Jcrop.css"));

            bundles.Add(new StyleBundle(Links.Bundles.Styles.Lightbox)
                .Include("~/Content/lightbox.css"));

            bundles.Add(new StyleBundle("~/Content/kendo/css").
                Include("~/Content/kendo/kendo.common.min.css",
                    "~/Content/kendo/kendo.default.min.css"));

            bundles.Add(new StyleBundle(Links.Bundles.Styles.JqueryPushMenu).
                Include("~/Content/jPushMenu.css"));

            bundles.Add(new StyleBundle(Links.Bundles.Styles.JqueryNanoScroller).
                Include("~/Content/nanoscroller.css"));

            bundles.Add(new StyleBundle(Links.Bundles.Styles.FontAwesome).
                Include("~/Content/font-awesome.min.css",
                    "~/Content/font-awesome.css"));

            bundles.Add(new StyleBundle(Links.Bundles.Styles.Site).
                Include("~/Content/site.css"));

            /////////////////// Scripts ////////////////////////
            bundles.Add(new ScriptBundle(Links.Bundles.Scripts.Flatdream)
                .Include("~/Scripts/behaviour/core.js",
                    "~/Scripts/behaviour/dashboard.js",
                    "~/Scripts/behaviour/dashboard2.js",
                    "~/Scripts/behaviour/ui-buttons.js"));

            bundles.Add(new ScriptBundle(Links.Bundles.Scripts.Json2)
                .Include("~/Scripts/json2.js"));

            bundles.Add(new ScriptBundle(Links.Bundles.Scripts.Jquery)
                .Include("~/Scripts/jquery-{version}.js",
                    "~/Scripts/jquery-migrate-{version}.js",
                    "~/Scripts/jquery-migrate-{version}.js",
                    "~/Scripts/jquery-ui-{version}.custom.js",
                    "~/Scripts/jquery-ui-{version}.custom.min.js"));

            bundles.Add(new ScriptBundle(Links.Bundles.Scripts.JqueryValidation)
                .Include("~/Scripts/jquery.validate.js",
                    "~/Scripts/jquery.validate.unobtrusive.js",
                    "~/Scripts/jquery.unobtrusive-ajax.js",
                    "~/Scripts/localization/methods_pt.js",
                    "~/Scripts/localization/messages_pt_BR.js"));

            bundles.Add(new ScriptBundle(Links.Bundles.Scripts.JqueryGlobalize)
                .Include("~/Scripts/jquery.globalize/globalize.js",
                    "~/Scripts/jquery.globalize/cultures/globalize.culture.pt-BR.js"));

            bundles.Add(new ScriptBundle(Links.Bundles.Scripts.Bootstrap)
                .Include("~/Scripts/bootstrap.js",
                    "~/Scripts/bootstrap-datepicker.js"));

            bundles.Add(new ScriptBundle(Links.Bundles.Scripts.Modernizr)
                .Include("~/Scripts/modernizr-{version}.js",
                    "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle(Links.Bundles.Scripts.Kendo)
                .Include("~/Scripts/kendo/kendo.web.js",
                    "~/Scripts/kendo/kendo.aspnetmvc.js",
                    "~/Scripts/kendo/cultures/kendo.culture.pt-BR.js"));

            bundles.Add(new ScriptBundle(Links.Bundles.Scripts.JasnyBootstrap)
                .Include("~/Scripts/jasny-bootstrap.js"));

            bundles.Add(new ScriptBundle(Links.Bundles.Scripts.JqueryForm)
                .Include("~/Scripts/jquery.form.js"));

            bundles.Add(new ScriptBundle(Links.Bundles.Scripts.ArquivoUpload)
                .Include("~/js/arquivoupload.js"));

            bundles.Add(new ScriptBundle(Links.Bundles.Scripts.FotoUpload)
                .Include("~/Scripts/jquery.Jcrop.js",
                    "~/js/fotoupload.js"));

            bundles.Add(new ScriptBundle(Links.Bundles.Scripts.JqueryColor)
                .Include("~/Scripts/jquery.color-{version}.js"));

            bundles.Add(new ScriptBundle(Links.Bundles.Scripts.FashionErp)
                .Include("~/js/utilitario.js",
                    "~/js/ajax.js",
                    "~/js/formatacao.js",
                    "~/js/validacao.js",
                    "~/js/inicializacao.js",
                    "~/js/icons-control.js"));

            bundles.Add(new ScriptBundle(Links.Bundles.Scripts.PdfObject)
                .Include("~/Scripts/pdfobject.js"));

            bundles.Add(new ScriptBundle(Links.Bundles.Scripts.Lightbox)
                .Include("~/Scripts/lightbox-{version}.js"));

            bundles.Add(new StyleBundle(Links.Bundles.Scripts.JqueryPushMenu).
                Include("~/Scripts/jPushMenu.js"));

            bundles.Add(new StyleBundle(Links.Bundles.Scripts.JqueryNanoScroller).
                Include("~/Scripts/jquery.nanoscroller.js",
                    "~/Scripts/jquery.nanoscroller.min.js"));

            bundles.Add(new StyleBundle(Links.Bundles.Scripts.JqueryCookie).
                Include("~/Scripts/jquery.cookie.js"));
            //
            #endregion
        }
    }
}