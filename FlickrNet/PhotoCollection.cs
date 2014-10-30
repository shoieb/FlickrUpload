﻿using System;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

namespace FlickrNet
{
    /// <remarks/>
    public sealed class PhotoCollection : PagedPhotoCollection, IFlickrParsable
    {
        void IFlickrParsable.Load(XmlReader reader)
        {
            if (reader.LocalName != "photos")
                UtilityMethods.CheckParsingException(reader);

            while (reader.MoveToNextAttribute())
            {
                switch (reader.LocalName)
                {
                    case "total":
                        Total = String.IsNullOrEmpty(reader.Value) ? 0 : int.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "perpage":
                    case "per_page":
                        PerPage = String.IsNullOrEmpty(reader.Value) ? 0 : int.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "page":
                        Page = String.IsNullOrEmpty(reader.Value) ? 0 : int.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "pages":
                        Pages = String.IsNullOrEmpty(reader.Value) ? 0 : int.Parse(reader.Value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    default:
                        UtilityMethods.CheckParsingException(reader);
                        break;

                }
            }

            reader.Read();

            while (reader.LocalName == "photo")
            {
                Photo p = new Photo();
                ((IFlickrParsable)p).Load(reader);
                if (!String.IsNullOrEmpty(p.PhotoId)) Add(p);
            }

            // Skip to next element (if any)
            reader.Skip();

        }

    }
}