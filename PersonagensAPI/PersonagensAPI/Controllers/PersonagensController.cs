using PersonagensDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PersonagensAPI.Controllers
{
    public class PersonagensController : ApiController
    {
        public IEnumerable<Personagens> Get()
        {
            using (PersonagensEntities entities = new PersonagensEntities())
            {
                return entities.Personagens.ToList();
            }
        }

        public HttpResponseMessage Get(int id)
        {
            using (PersonagensEntities entities = new PersonagensEntities())
            {
                var entity = entities.Personagens.FirstOrDefault(e => e.ID == id);

                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Funcionario com o id: {id.ToString()} não foi encontrado");
                }
            }
        }

        public HttpResponseMessage Post([FromBody] Personagens personagem)
        {
            try
            {
                using (var entities = new PersonagensEntities())
                {
                    entities.Personagens.Add(personagem);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, personagem);
                    message.Headers.Location = new Uri(Request.RequestUri + personagem.ID.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (PersonagensEntities entities = new PersonagensEntities())
                {
                    var entity = entities.Personagens.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"O personagem com o id: {entity.ID.ToString()} não foi encontrado!");
                    }
                    else
                    {
                        entities.Personagens.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody] Personagens personagem)
        {
            try
            {
                using (PersonagensEntities entities = new PersonagensEntities())
                {
                    var entity = entities.Personagens.FirstOrDefault(e => e.ID == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"O personagem com o id: {entity.ID.ToString()} não foi encontrado");
                    }
                    else
                    {
                        entity.FotoUrl = personagem.FotoUrl;
                        entity.Nome = personagem.Nome;
                        entity.Descricao = personagem.Descricao;
                        entity.NomeReal = personagem.NomeReal;
                        entity.Genero = personagem.Genero;
                        entity.Altura = personagem.Altura;
                        entity.Peso = personagem.Peso;
                        entity.Poderes = personagem.Poderes;
                        entity.Habilidades = personagem.Habilidades;
                        entity.Afiliacoes = personagem.Afiliacoes;
                        entity.Origem = personagem.Origem;

                        entities.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
