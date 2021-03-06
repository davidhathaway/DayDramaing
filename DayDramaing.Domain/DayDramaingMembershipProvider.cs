﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DayDramaing.Domain.Repositories;
using Innovations.Core.Patterns;
using Innovations.Core.Providers;

namespace DayDramaing.Domain
{
    public class DayDramaingMembershipProvider : InnoMembershipProvidor
    {
        public override IUserRepositoryBase GetRepository()
        {
            return new UserRepository();
        }
    }
}
