using Azure.Core;
using Bogus;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using ProdutoApp.API.Models;
using ProdutoApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProdutoApp.Tests
{
    public class ProdutosTest
    {
        [Fact]
        public async Task<ProdutoGetResponseModel> CadastrarProdutos_Test()
        {
            #region Gerar os dados do teste

            var faker = new Faker("pt_BR");
            var request = new ProdutoPostRequestModel
            {
                Nome = faker.Commerce.ProductName(), // Corrigido para gerar uma sentença
                Descricao = faker.Lorem.Sentences(1),
                DataHora = faker.Date.Between(DateTime.Now.AddDays(-7), DateTime.Now.AddDays(7)),
                Preco = Double.Parse(faker.Commerce.Price(min: 1, max: 1000, decimals: 2)), // Gera um preço entre 10 e 100
                Quantidade = faker.Random.Int(1, 150), // Definido um intervalo para uma quantidade mais realista
                Estado = faker.Random.Int(0, 3) // Estado entre 0 e 3
            };

            //converter / serializar os dados em JSON
            var jsonRequest = new StringContent(JsonConvert.SerializeObject(request),
                                Encoding.UTF8, "application/json");

            #endregion

            #region Enviando a requisição POST para a API

            var client = new WebApplicationFactory<Program>().CreateClient();

            var result = await client.PostAsync("/api/produtos", jsonRequest);

            #endregion

            #region Verificar o resultado

            result.StatusCode.Should().Be(HttpStatusCode.Created);

            //ler os dados obtidos na api
            var jsonResult = result.Content.ReadAsStringAsync().Result;

            var response = JsonConvert.DeserializeObject<ProdutoGetResponseModel>(jsonResult);

            //Fazer critérios para cada campo obtido
            response?.Id.Should().NotBeEmpty();
            response?.Nome.Should().Be(request.Nome);
            response?.Descricao.Should().Be(request.Descricao);
            response?.DataHora.Should().Be(request.DataHora);
            response?.Preco.Should().Be(request.Preco);
            response?.Quantidade.Should().Be(request.Quantidade);
            response?.CadastradoEm.Should().NotBeNull();
            response?.Estado.Should().Be(request.Estado);
            response?.Ativo.Should().Be(1);

            #endregion

            return response;
        }

        [Fact]
        public async Task AtualizarProdutos_Test()
        {
            var produto = await CadastrarProdutos_Test();

            var faker = new Faker("pt_BR");

            var request = new ProdutoPutRequestModel
            {
                Id = produto.Id,
                Nome = faker.Commerce.ProductName(),
                Descricao = faker.Lorem.Sentences(1),
                DataHora = faker.Date.Between(DateTime.Now.AddDays(-7), DateTime.Now.AddDays(7)),
                Preco = Double.Parse(faker.Commerce.Price(min: 1, max: 1000, decimals: 2)),
                Quantidade = faker.Random.Int(1, 150),
                Estado = faker.Random.Int(0, 3)
            };

            var jsonRequest = new StringContent(JsonConvert.SerializeObject(request),
                                Encoding.UTF8, "application/json");

            //instanciando a API através da classe Program (classe de inicialização)
            var client = new WebApplicationFactory<Program>().CreateClient();

            //executando uma chamada para o serviço PUT de cadastro de tarefas
            var result = await client.PutAsync("/api/produtos", jsonRequest);

            //verificando se a resposta é igual a 200 (OK)
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            //lendo os dados obtidos da API
            var jsonResult = result.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<ProdutoGetResponseModel>(jsonResult);

            response?.Id.Should().Be(request.Id);
            response?.Nome.Should().Be(request.Nome);
            response?.Descricao.Should().Be(request.Descricao);
            response?.DataHora.Should().Be(request.DataHora);
            response?.Preco.Should().Be(request.Preco);
            response?.Quantidade.Should().Be(request.Quantidade);
            response?.Estado.Should().Be(request.Estado);
            response?.CadastradoEm.Should().NotBeNull();
            response?.UltimaAtualizacaoEm.Should().NotBeNull();
            response?.Ativo.Should().Be(1);
        }

        [Fact]
        public async Task ExcluirProdutos_Test()
        {
            var produto = await CadastrarProdutos_Test();

            var client = new WebApplicationFactory<Program>().CreateClient();

            var result = await client.DeleteAsync($"/api/produtos/{produto.Id}");

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            var jsonResult = await result.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<ProdutoGetResponseModel>(jsonResult);

            response?.Id.Should().Be(produto.Id);
            response?.Nome.Should().Be(produto.Nome);
            response?.Descricao.Should().Be(produto.Descricao);
            response?.DataHora.Should().Be(produto.DataHora);
            response?.Estado.Should().Be(produto.Estado);
            response?.ExcluidoEm.Should().NotBeNull(); 
        }


        [Fact]
        public async Task ConsultarProdutosPorDatas_Test()
        {
            #region Gerar os dados do teste

            //realizar o cadastro de um produto
            var produto = await CadastrarProdutos_Test();

            //definindo as datas para pesquisar os produtos
            var dataInicio = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            var dataFim = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");

            #endregion

            #region Enviando a requisição GET para a API

            //instanciando a API através da classe Program (classe de inicialização)
            var client = new WebApplicationFactory<Program>().CreateClient();

            //fazendo a requisição para consultar as tarefas
            var result = await client.GetAsync("/api/produtos/" + dataInicio + "/" + dataFim);

            #endregion

            #region Verificar o resultado

            result.StatusCode.Should().Be(HttpStatusCode.OK);

            //deserializando os dados obtidos no teste
            var jsonResult = result.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<List<ProdutoGetResponseModel>>(jsonResult);

            //procurar a tarefa cadastrada dentro da lista obtida da API
            var produtoObtido = response?.FirstOrDefault(p => p.Id == produto.Id);

            //comparando se os campos do produto estão corretos
            produtoObtido?.Id.Should().Be(produto.Id);
            produtoObtido?.Nome.Should().Be(produto.Nome);
            produtoObtido?.Descricao.Should().Be(produto.Descricao);
            produtoObtido?.DataHora.Should().Be(produto.DataHora);
            produtoObtido?.Preco.Should().Be(produto.Preco);
            produtoObtido?.Quantidade.Should().Be(produto.Quantidade);
            produtoObtido?.Estado.Should().Be(produto.Estado);

            #endregion
        }

        [Fact]
        public async Task ObterProdutoPorId_Test()
        {
            #region Gerar os dados do teste

            //realizar o cadastro de um produto
            var produto = await CadastrarProdutos_Test();

            #endregion

            #region Enviando a requisição GET para a API

            //instanciando a API através da classe Program (classe de inicialização)
            var client = new WebApplicationFactory<Program>().CreateClient();

            //fazendo a requisição para consulta a tarefa
            var result = await client.GetAsync("/api/produtos/" + produto.Id);

            #endregion

            #region Verificar o resultado

            //verificando se a resposta é igual a 200 (OK)
            result.StatusCode.Should().Be(HttpStatusCode.OK);

            //lendo os dados obtidos da API
            var jsonResult = result.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<ProdutoGetResponseModel>(jsonResult);

            //critérios para cada campo obtido
            response?.Id.Should().Be(produto.Id);
            response?.Nome.Should().Be(produto.Nome);
            response?.Descricao.Should().Be(produto.Descricao);
            response?.DataHora.Should().Be(produto.DataHora);
            response?.Preco.Should().Be(produto.Preco);
            response?.Quantidade.Should().Be(produto.Quantidade);
            response?.Estado.Should().Be(produto.Estado);

            #endregion
        }
    }
}
