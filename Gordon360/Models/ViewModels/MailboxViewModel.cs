﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gordon360.Models.ViewModels
{
    public class MailboxViewModel
    {
        public string Combination { get; set;  }

        public static implicit operator MailboxViewModel(Mailboxes req)
        {
            MailboxViewModel vm = new MailboxViewModel
            {
                Combination = (req != null) ? req.Combination ?? "" : "",
            };

            return vm;
        }
    }
}