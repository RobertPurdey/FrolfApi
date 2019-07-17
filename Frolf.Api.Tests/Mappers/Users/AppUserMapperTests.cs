using Domain.Entities;
using Frolf.Api.Mappers.Users;
using Frolf.Api.Models.Users;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frolf.Api.Tests.Mappers.Users
{
    [TestFixture]
    public class UserMapperTests
    {
        #region Setup
        
        AppUserMapper mapper;

        AppUser entity;
        AppUserModel model;

        [SetUp]
        public void Setup()
        {
            SetupEntity();
            SetupModel();

            mapper = new AppUserMapper();
        }

        private void SetupEntity()
        {          
            entity = new AppUser
            {
                EntityKey   = Guid.NewGuid(),
                LoginName   = "Dogbert",
                Handle      = "Dilbert's Master",
                Password    = "simple123simpletons",
                FriendCode  = "ploys123"
            };


        }

        private void SetupModel()
        {
            model = new AppUserModel
            {
                IdKey       = Guid.NewGuid(),
                LoginName   = "Catbert",
                Handle      = "Dogbert's Master",
                Password    = "hellonearth"
            };
        }

        #endregion

        [Test]
        public void AppUserMapper_MapToEntity_ValuesAreExpectedModel()
        {
            var mappedEntity = new AppUser();

            mapper.MapToEntity( model, mappedEntity );

            Assert.AreEqual( model.IdKey,      mappedEntity.EntityKey );
            Assert.AreEqual( model.LoginName,  mappedEntity.LoginName );
            Assert.AreEqual( model.Handle,     mappedEntity.Handle    );
            Assert.AreEqual( model.Password,   mappedEntity.Password  );
        }

        [Test]
        public void AppUserMapper_MapToModel_ValuesAreExpectedModel()
        {
            var mappedModel = new AppUserModel();

            mapper.MapToApiModel(mappedModel, entity);
             
            Assert.AreEqual( entity.EntityKey,   mappedModel.IdKey      );
            Assert.AreEqual( entity.LoginName,   mappedModel.LoginName  );
            Assert.AreEqual( entity.Handle,      mappedModel.Handle     );
            Assert.AreEqual( entity.Password,    mappedModel.Password   );
            Assert.AreEqual( entity.FriendCode,  mappedModel.FriendCode );
        }
    }
}
