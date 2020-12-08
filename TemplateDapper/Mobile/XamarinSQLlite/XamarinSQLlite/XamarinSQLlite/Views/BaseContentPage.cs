using Prism;
using Prism.DryIoc;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinSQLlite.Views
{
    public class BaseContentPage : ContentPage
    {
        public BaseContentPage()
        {
            if (PrismApplicationBase.Current is PrismApplication app)
            {
                EventAggregator = (IEventAggregator)app.Container.Resolve(typeof(IEventAggregator));
            }
        }

        protected IEventAggregator EventAggregator { get; }
    }
}
