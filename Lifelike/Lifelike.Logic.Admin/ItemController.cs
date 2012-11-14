using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lifelike.Kernel;
using Lifelike.Kernel.Entities;
using Lifelike.Kernel.EntityLogic;

namespace Lifelike.Logic.Admin
{
	public class ItemController : ApiController
	{

		

		// GET api/<controller>
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET api/<controller>/5
		public Item Get(Guid id)
		{
			var session = Database.Context.CurrentSession;
			return ItemLogic.LoadBy(session, (p => p.Id == id));
		}

		// POST api/<controller>
		public void Post([FromBody]string value)
		{
		}

		// PUT api/<controller>/5
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/<controller>/5
		public void Delete(int id)
		{
		}
	}
}