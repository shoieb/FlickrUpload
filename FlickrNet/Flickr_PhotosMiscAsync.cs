﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FlickrNet
{
    public partial class Flickr
    {
        /// <summary>
        /// Rotates a photo on Flickr.
        /// </summary>
        /// <remarks>
        /// Does not rotate the original photo.
        /// </remarks>
        /// <param name="photoId">The ID of the photo.</param>
        /// <param name="degrees">The number of degrees to rotate by. Valid values are 90, 180 and 270.</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PhotosTransformRotateAsync(string photoId, int degrees, Action<FlickrResult<NoResponse>> callback)
        {
            if (photoId == null)
                throw new ArgumentNullException("photoId");
            if (degrees != 90 && degrees != 180 && degrees != 270)
                throw new ArgumentException("Must be 90, 180 or 270", "degrees");

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.transform.rotate");
            parameters.Add("photo_id", photoId);
            parameters.Add("degrees", degrees.ToString(System.Globalization.NumberFormatInfo.InvariantInfo));

            GetResponseAsync<NoResponse>(parameters, callback);
        }

        /// <summary>
        /// Checks the status of one or more asynchronous photo upload tickets.
        /// </summary>
        /// <param name="tickets">A list of ticket ids</param>
        /// <param name="callback">Callback method to call upon return of the response from Flickr.</param>
        public void PhotosUploadCheckTicketsAsync(string[] tickets, Action<FlickrResult<TicketCollection>> callback)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("method", "flickr.photos.upload.checkTickets");
            parameters.Add("tickets", String.Join(",", tickets));

            GetResponseAsync<TicketCollection>(parameters, callback);
        }

    }
}
