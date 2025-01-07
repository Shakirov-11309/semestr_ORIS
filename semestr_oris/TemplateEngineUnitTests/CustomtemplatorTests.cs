﻿using MyHtttpServer.Core.Templator;
using TemplateEngineUnitTests.Models;

namespace MyHttpServer.UnitTests.Core.Templator
{
    [TestClass]
    public class CustomTemplatorTests
    {

        [TestMethod]
        public void GetHtmlByTemplate_When_NameIsnotNull_ResultSuccess()
        {
            // Arrange
            ICustomTemplator customTemplator = new CustomTemplator();
            string template = "<label>name</label><p>{name}</p>";
            string name = "Тимерхан";


            // Act
            var result = customTemplator.GetHtmlByTemplate(template, name);

            // Assert
            Assert.AreEqual("<label>name</label><p>Тимерхан</p>", result);
        }

        [TestMethod]
        public void GetHtmlByTemplate_When_UserIsnotNull_ResultSuccess()
        {
            // Arrange
            ICustomTemplator customTemplator = new CustomTemplator();
            string template = "<p>Ваш логин: {login}; Ваш пароль: {password};</p>";
            var user = new Person
            {
                Login = "test@test.ru",
                Password = "passWord",
                Name = "Ильхам"
            };

            // Act
            var result = customTemplator.GetHtmlByTemplate(template, user);

            // Assert
            Assert.AreEqual("<p>Ваш логин: test@test.ru; Ваш пароль: passWord;</p>", result);
        }

        [TestMethod]
        public void GetHtmlByTemplate_When_ObjectIsnotNull_ResultSuccess()
        {
            // Arrange
            ICustomTemplator customTemplator = new CustomTemplator();
            string template = "Привет {name}! <p>Ваш логин: {login}; Ваш пароль: {password}; Мы очень рады с вами познакомится дорогой {name}</p>";
            var person = new Person
            {
                Login = "test@test.ru",
                Password = "passWord",
                Name = "Ильхам"
            };

            // Act
            var result = customTemplator.GetHtmlByTemplate<Person>(template, person);

            // Assert
            Assert.AreEqual("Привет Иван! <p>Ваш логин: test@test.ru; Ваш пароль: passWord; Мы очень рады с вами познакомится дорогой Иван!</p>", result);
        }
    }
}
