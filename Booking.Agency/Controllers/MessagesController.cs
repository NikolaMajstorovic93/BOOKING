﻿using Booking.Agency;
using Booking.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Web;
using System.Web.Http.Filters;
using Booking.Agency.Base.Web.Http;
using Booking.Agency.Base.Data;



namespace Booking.Agency.Controllers
{
    public class MessagesController : BaseApiController, IBaseActions
    {

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetAll()
        {
            if (ModelState.IsValid)
            {
                BaseRepository bs = new BaseRepository();

                List<Reservation> reservationsList = bs.GetAllReservationsForUserId(GetUserId());

                return Ok(reservationsList, HttpStatusCode.OK, "Successfully GetAll");
            }
            else
            {
                return Error(HttpStatusCode.NotAcceptable, ModelState);
            }
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage Get(Guid objId)
        {
            if (ModelState.IsValid)
            {
                BaseRepository bs = new BaseRepository();

                List<Reservation> reservationsList = bs.GetAllReservationsForAccomodationId(objId);
                return Ok(reservationsList, HttpStatusCode.OK, "Successfully GetAll");
            }
            else
            {
                return Error(HttpStatusCode.NotAcceptable, ModelState);
            }
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage Create(dynamic model)
        {
            SentMessage message = MapJsonToModelObject<SentMessage>(model, false);

            if (ModelState.IsValid)
            {
                BaseRepository bs = new BaseRepository();
                bs.SendMessage(message);

                //Return reservations with fresh messages
                List<Reservation> reservationsList = bs.GetAllReservationsForUserId(GetUserId());

                return Ok(reservationsList, HttpStatusCode.OK, "Message Successfully Sent");
            }
            else
            {
                return Error(HttpStatusCode.NotAcceptable, ModelState);
            }
        }

        [System.Web.Http.HttpPut]
        public HttpResponseMessage Update(dynamic model)
        {
            Comment comment = MapJsonToModelObject<Comment>(model, false);

            if (ModelState.IsValid)
            {
                BaseRepository bs = new BaseRepository();

                bs.ApproveComment(comment);
                List<Comment> commentsList = bs.GetAllComments();
                return Ok(null, HttpStatusCode.OK, "Successfully updated");
            }
            else
            {
                return Error(HttpStatusCode.NotAcceptable, ModelState);
            }
        }


        [System.Web.Http.HttpPut]
        public HttpResponseMessage MarkMessagesAsSeen(dynamic model)
        {
            List<SentMessage> sentMessagesList = MapJsonToModelArrayObjects<List<SentMessage>>(model, false);

            if (ModelState.IsValid)
            {
                BaseRepository bs = new BaseRepository();

                foreach (SentMessage message in sentMessagesList)
                {
                    message.MessageSeen = 1;
                    bs.UpdateMessage(message);
                }

                return Ok(null, HttpStatusCode.OK, "Successfully updated");
            }
            else
            {
                return Error(HttpStatusCode.NotAcceptable, ModelState);
            }
        }
        

        [System.Web.Http.HttpDelete]
        public HttpResponseMessage Delete(Guid objId)
        {
            if (ModelState.IsValid)
            {
                BaseRepository bs = new BaseRepository();
             
                return Ok(null, HttpStatusCode.OK, "Successfully deleted");
            }
            else
            {
                return Error(HttpStatusCode.NotAcceptable, ModelState);
            }
        }

    }
}
