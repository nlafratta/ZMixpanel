﻿using System;
using System.Text;

namespace Zirpl.Mixpanel.Web.Mvc.JavaScript
{
    public class TrackFormsBuilder :CallBuilderBase
    {
        private String _eventName;
        private String _domSelector;
        private readonly PropertiesBuilder<object> _propertiesBuilder; 

        public TrackFormsBuilder()
        {
            this._propertiesBuilder = new PropertiesBuilder<object>(this);
        }

        public TrackFormsBuilder(String instanceName)
            :base(instanceName)
        {
            this._propertiesBuilder = new PropertiesBuilder<object>(this);
        }

        public PropertiesBuilder<Object> Properties()
        {
            return this._propertiesBuilder;
        }

        public TrackFormsBuilder EventName(String value)
        {
            this._eventName = value;
            return this;
        }

        public TrackFormsBuilder DomSelector(String value)
        {
            this._domSelector = value;
            return this;
        }

        public override string ToHtmlString()
        {
            if (String.IsNullOrEmpty(this._eventName))
            {
                throw new InvalidOperationException("Cannot call ToHtmlString without EventName set");
            }
            if (String.IsNullOrEmpty(this._domSelector))
            {
                throw new InvalidOperationException("Cannot call ToHtmlString without EventName set");
            }

            var sb = new StringBuilder();
            sb.AppendFormat("{0}.track_forms(\"{1}\", \"{2}\"", base.ToHtmlString(), this._domSelector, this._eventName);
 
            var properties = this.Properties().ToPartialJavaScriptString();
            if (!String.IsNullOrEmpty(properties))
            {
                sb.AppendFormat(", {0}", properties);
            }
            sb.Append(");");
            return sb.ToString();
        }
    }
}
