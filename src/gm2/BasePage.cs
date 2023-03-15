using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace System.gm
{
    public class BasePage : System.Web.UI.Page
    {
        private ViewStateCompressor _viewStateCompressor;

        public BasePage()
            : base()
        {
            _viewStateCompressor = new ViewStateCompressor(this);
        }

        protected override PageStatePersister PageStatePersister
        {
            get
            {
                return _viewStateCompressor;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
    }

    public class ViewStateCompressor : PageStatePersister
    {
        public ViewStateCompressor(Page page)
            : base(page)
        {
        }

        private LosFormatter _stateFormatter;

        protected new LosFormatter StateFormatter
        {
            get
            {
                if (this._stateFormatter == null)
                {
                    this._stateFormatter = new LosFormatter();
                }
                return this._stateFormatter;
            }
        }

        public override void Save()
        {
            using (StringWriter writer = new StringWriter(System.Globalization.CultureInfo.InvariantCulture))
            {
                StateFormatter.Serialize(writer, new Pair(base.ViewState, base.ControlState));
                byte[] bytes = Convert.FromBase64String(writer.ToString());

                bytes = Compressor.Compress(bytes);

                ScriptManager.RegisterHiddenField(Page, "__PIT", Convert.ToBase64String(bytes));
            }
        }

        public override void Load()
        {
            byte[] bytes = Convert.FromBase64String(base.Page.Request.Form["__PIT"]);

            bytes = gm.Compressor.Decompress(bytes);

            Pair p = ((Pair)(StateFormatter.Deserialize(Convert.ToBase64String(bytes))));
            base.ViewState = p.First;
            base.ControlState = p.Second;
        }
    }
}