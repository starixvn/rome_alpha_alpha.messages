using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alpha.Messages.Tests.Helpers;

namespace Alpha.Messages.Tests.Database
{
    public static class MessageDatabase
    {
        private static DataSet messageData = FileHelper.LoadDataSet(@"\DataBase\MessageHasData.xml");

        public static DataSet MessageData => messageData;
    }
}
