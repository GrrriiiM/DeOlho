using System;

namespace DeOlho.ETL.dadosabertos_camara_leg_br.UnitTests
{
    public class Constants
    {
        

        public class Url
        {
            public const string ROOT = "http://teste.com/api/"; 
            public const string PARTIDO = "/partidos";
            public const string DETAIL_PARTIDO = "/partidos/{0}";
 
            public const string LEGISLATURA = "/legislaturas";

            public const string DEPUTADO = "/deputados";
            public const string DETAIL_DEPUTADO = "/deputado/{0}";

            public const string DETAIL_DESPESA = "deputados/{0}/despesas?ano={1}&mes={2}&itens=99999&ordem=ASC&ordenarPor=ano";
        }

        public class Json 
        {
            
            public const string PARTIDO = @"{'dados':[{'id':1},{'id':2}]}";
            public const string PARTIDO_1 = @"
    {
    'dados': {
        'id': 1,
        'sigla': 'NOVO',
        'nome': 'Partido Novo',
        'uri': 'https://dadosabertos.camara.leg.br/api/v2/partidos/37901',
        'status': {
        'data': '2019-02-01T22:19',
        'idLegislatura': '56',
        'situacao': 'Ativo',
        'totalPosse': '8',
        'totalMembros': '8',
        'uriMembros': 'https://dadosabertos.camara.leg.br/api/v2/deputados?legislatura=56&partido=NOVO',
        'lider': {
            'uri': 'https://dadosabertos.camara.leg.br/api/v2/deputados/156190',
            'nome': 'MARCEL VAN HATTEM',
            'siglaPartido': 'NOVO',
            'uriPartido': 'https://dadosabertos.camara.leg.br/api/v2/partidos/37901',
            'uf': 'RS',
            'idLegislatura': 56,
            'urlFoto': 'http://www.camara.gov.br/internet/deputado/bandep/156190.jpg'
        }
        },
        'numeroEleitoral': null,
        'urlLogo': 'http://www.camara.leg.br/internet/Deputado/img/partidos/NOVO.gif',
        'urlWebSite': null,
        'urlFacebook': null
    },
    'links': [
        {
        'rel': 'self',
        'href': 'https://dadosabertos.camara.leg.br/api/v2/partidos/37901'
        }
    ]
    }            
                ";
            public const string PARTIDO_2 = @"
    {
    'dados': {
        'id': 2,
        'sigla': 'PT',
        'nome': 'Partido dos Trabalhadores',
        'uri': 'https://dadosabertos.camara.leg.br/api/v2/partidos/36844',
        'status': {
        'data': '2019-02-01T14:12',
        'idLegislatura': '56',
        'situacao': 'Ativo',
        'totalPosse': '54',
        'totalMembros': '55',
        'uriMembros': 'https://dadosabertos.camara.leg.br/api/v2/deputados?legislatura=56&partido=PT',
        'lider': {
            'uri': 'https://dadosabertos.camara.leg.br/api/v2/deputados/74400',
            'nome': 'PAULO PIMENTA',
            'siglaPartido': 'PT',
            'uriPartido': 'https://dadosabertos.camara.leg.br/api/v2/partidos/36844',
            'uf': 'RS',
            'idLegislatura': 56,
            'urlFoto': 'http://www.camara.gov.br/internet/deputado/bandep/74400.jpg'
        }
        },
        'numeroEleitoral': null,
        'urlLogo': 'http://www.camara.leg.br/internet/Deputado/img/partidos/PT.gif',
        'urlWebSite': null,
        'urlFacebook': null
    },
    'links': [
        {
        'rel': 'self',
        'href': 'https://dadosabertos.camara.leg.br/api/v2/partidos/36844'
        }
    ]
    }
                ";
       
            public const string LEGISLATURA = @"
            {
    'dados': [
        {
        'id': 56,
        'uri': 'https://dadosabertos.camara.leg.br/api/v2/legislaturas/56',
        'dataInicio': '2019-02-01',
        'dataFim': '2023-01-31'
        },
        {
        'id': 55,
        'uri': 'https://dadosabertos.camara.leg.br/api/v2/legislaturas/55',
        'dataInicio': '2015-02-01',
        'dataFim': '2019-01-31'
        }
    ]
}
            ";
        
            public const string DEPUTADO = @"
{'dados':[{'id':204536},{'id':92346}]}            
            ";
            public const string DEPUTADO_1 = @"
{
  'dados': {
    'id': 204536,
    'uri': 'https://dadosabertos.camara.leg.br/api/v2/deputados/204536',
    'nomeCivil': 'KIM PATROCA KATAGUIRI',
    'ultimoStatus': {
      'id': 204536,
      'uri': 'https://dadosabertos.camara.leg.br/api/v2/deputados/204536',
      'nome': 'KIM KATAGUIRI',
      'siglaPartido': 'DEM',
      'uriPartido': 'https://dadosabertos.camara.leg.br/api/v2/partidos/36769',
      'siglaUf': 'SP',
      'idLegislatura': 56,
      'urlFoto': 'https://www.camara.leg.br/internet/deputado/bandep/204536.jpg',
      'data': '2019-02-01T11:45',
      'nomeEleitoral': 'KIM KATAGUIRI',
      'gabinete': {
        'nome': '421',
        'predio': '4',
        'sala': '421',
        'andar': '4',
        'telefone': '3215-5421',
        'email': 'dep.kimkataguiri@camara.leg.br'
      },
      'situacao': 'Exercício',
      'condicaoEleitoral': 'Titular',
      'descricaoStatus': null
    },
    'cpf': '39313495864',
    'sexo': 'M',
    'urlWebsite': null,
    'redeSocial': [],
    'dataNascimento': '1996-01-28',
    'dataFalecimento': null,
    'ufNascimento': 'SP',
    'municipioNascimento': 'Salto',
    'escolaridade': 'Superior Incompleto'
  },
  'links': [
    {
      'rel': 'self',
      'href': 'https://dadosabertos.camara.leg.br/api/v2/deputados/204536'
    }
  ]
}
            ";
            public const string DEPUTADO_2 = @"
{
  'dados': {
    'id': 92346,
    'uri': 'https://dadosabertos.camara.leg.br/api/v2/deputados/92346',
    'nomeCivil': 'EDUARDO NANTES BOLSONARO',
    'ultimoStatus': {
      'id': 92346,
      'uri': 'https://dadosabertos.camara.leg.br/api/v2/deputados/92346',
      'nome': 'EDUARDO BOLSONARO',
      'siglaPartido': 'PSL',
      'uriPartido': 'https://dadosabertos.camara.leg.br/api/v2/partidos/36837',
      'siglaUf': 'SP',
      'idLegislatura': 56,
      'urlFoto': 'https://www.camara.leg.br/internet/deputado/bandep/92346.jpg',
      'data': '2019-02-01T11:45',
      'nomeEleitoral': 'EDUARDO BOLSONARO',
      'gabinete': {
        'nome': '350',
        'predio': '4',
        'sala': '350',
        'andar': '3',
        'telefone': '3215-5350',
        'email': 'dep.eduardobolsonaro@camara.leg.br'
      },
      'situacao': 'Exercício',
      'condicaoEleitoral': 'Titular',
      'descricaoStatus': null
    },
    'cpf': '10655365770',
    'sexo': 'M',
    'urlWebsite': null,
    'redeSocial': [],
    'dataNascimento': '1984-07-10',
    'dataFalecimento': null,
    'ufNascimento': 'RJ',
    'municipioNascimento': 'Rio de Janeiro',
    'escolaridade': 'Superior'
  },
  'links': [
    {
      'rel': 'self',
      'href': 'https://dadosabertos.camara.leg.br/api/v2/deputados/92346'
    }
  ]
}            
            ";
        
            public const string DESPESA_1 = @"
{
  'dados': [
    {
      'ano': 2019,
      'mes': 3,
      'tipoDespesa': 'Emissão Bilhete Aéreo',
      'codDocumento': 1641391,
      'tipoDocumento': 'Nota Fiscal',
      'codTipoDocumento': 0,
      'dataDocumento': '2019-03-08',
      'numDocumento': 'Bilhete: 2444321916',
      'valorDocumento': 1028.49,
      'urlDocumento': '',
      'nomeFornecedor': 'Cia Aérea - AVIANCA',
      'cnpjCpfFornecedor': '02575829000148',
      'valorLiquido': 1028.49,
      'valorGlosa': 0,
      'numRessarcimento': '0',
      'codLote': 0,
      'parcela': 0
    },
    {
      'ano': 2019,
      'mes': 3,
      'tipoDespesa': 'Emissão Bilhete Aéreo',
      'codDocumento': 1641392,
      'tipoDocumento': 'Nota Fiscal',
      'codTipoDocumento': 0,
      'dataDocumento': '2019-03-08',
      'numDocumento': 'Bilhete: 2444321917',
      'valorDocumento': 529.94,
      'urlDocumento': '',
      'nomeFornecedor': 'Cia Aérea - AVIANCA',
      'cnpjCpfFornecedor': '02575829000148',
      'valorLiquido': 529.94,
      'valorGlosa': 0,
      'numRessarcimento': '0',
      'codLote': 0,
      'parcela': 0
    }
  ]
}
            ";
            public const string DESPESA_2 = @"
{
  'dados': [
    {
      'ano': 2019,
      'mes': 3,
      'tipoDespesa': 'MANUTENÇÃO DE ESCRITÓRIO DE APOIO À ATIVIDADE PARLAMENTAR',
      'codDocumento': 6792046,
      'tipoDocumento': 'Nota Fiscal',
      'codTipoDocumento': 0,
      'dataDocumento': '2019-03-12',
      'numDocumento': '108749565',
      'valorDocumento': 1.89,
      'urlDocumento': '',
      'nomeFornecedor': 'ELETROPAULO METROPOLITANA DE SÃO PAULO S/A',
      'cnpjCpfFornecedor': '61695227000193',
      'valorLiquido': 1.89,
      'valorGlosa': 0,
      'numRessarcimento': '',
      'codLote': 1578702,
      'parcela': 0
    },
    {
      'ano': 2019,
      'mes': 3,
      'tipoDespesa': 'MANUTENÇÃO DE ESCRITÓRIO DE APOIO À ATIVIDADE PARLAMENTAR',
      'codDocumento': 6792058,
      'tipoDocumento': 'Nota Fiscal',
      'codTipoDocumento': 0,
      'dataDocumento': '2019-03-12',
      'numDocumento': '108760568',
      'valorDocumento': 35.34,
      'urlDocumento': '',
      'nomeFornecedor': 'ELETROPAULO METROPOLITANA DE SÃO PAULO S/A',
      'cnpjCpfFornecedor': '61695227000193',
      'valorLiquido': 35.13,
      'valorGlosa': 0.21,
      'numRessarcimento': '',
      'codLote': 1578702,
      'parcela': 0
    }
  ]
}            
            ";
  
        }

        public class Db
        {
            public const string TABLE_PARTIDO = "PartidoTableName";
            public const string CREATE_PARTIDO = @"
CREATE TABLE PartidoTableName (
Id int NOT NULL,Sigla varchar(255) ,
Nome varchar(255) ,
Data date ,
LegislaturaId int NOT NULL,
Situacao varchar(255) ,
TotalPosse int NOT NULL,
TotalMembros int NOT NULL,
LiderId varchar(255) ,
UrlFacebook varchar(255) ,
UrlLogo varchar(255) ,
UrlWebSite varchar(255) 
);"; 
            public const string INSERT_PARTIDO = @"
INSERT INTO PartidoTableName 
(Id,Sigla,Nome,Data,LegislaturaId,Situacao,TotalPosse,TotalMembros,LiderId,UrlFacebook,UrlLogo,UrlWebSite) 
VALUES 
(1,'NOVO','Partido Novo','2019-02-01 22:19:00',56,'Ativo',8,8,'https://dadosabertos.camara.leg.br/api/v2/deputados/156190',NULL,'http://www.camara.leg.br/internet/Deputado/img/partidos/NOVO.gif',NULL),
(2,'PT','Partido dos Trabalhadores','2019-02-01 14:12:00',56,'Ativo',54,55,'https://dadosabertos.camara.leg.br/api/v2/deputados/74400',NULL,'http://www.camara.leg.br/internet/Deputado/img/partidos/PT.gif',NULL);
";
            public const string DELETE_PARTIDO = @"DELETE FROM PartidoTableName";

            public const string TABLE_LEGISLATURA = "LegislaturaTableName";
            public const string CREATE_LEGISLATURA = @"
CREATE TABLE LegislaturaTableName (
Id int NOT NULL,
DataInicio date NOT NULL,
DataFim date NOT NULL
);";
            public const string INSERT_LEGISLATURA = @"
INSERT INTO LegislaturaTableName 
(Id,DataInicio,DataFim) 
VALUES 
(56,'2019-02-01 00:00:00','2023-01-31 00:00:00'),
(55,'2015-02-01 00:00:00','2019-01-31 00:00:00');            
            ";
            public const string DELETE_LEGISLATURA = @"DELETE FROM LegislaturaTableName";

            public const string TABLE_DEPUTADO = @"deputadoTableName";
            public const string CREATE_DEPUTADO = @"
CREATE TABLE deputadoTableName (
Id int NOT NULL,
CPF varchar(255) ,
DataFalecimento date ,
DataNascimento date NOT NULL,
Escolaridade varchar(255) ,
MunicipioNascimento varchar(255) ,
NomeCivil varchar(255) ,
RedeSocial varchar(255) ,
Sexo varchar(255) ,
UFNascimento varchar(255) ,
URLWebsite varchar(255) ,
LegislaturaId int NOT NULL,
Nome varchar(255) ,
NomeEleitoral varchar(255) ,
SiglaPartido varchar(255) ,
SiglaUf varchar(255) ,
Situacao varchar(255) ,
PartidoId int NOT NULL,
URLFoto varchar(255) ,
CondicaoEleitoral varchar(255) ,
Data date ,
DescricaoStatus varchar(255) ,
GabineteAndar varchar(255) ,
GabineteEmail varchar(255) ,
GabineteNome varchar(255) ,
GabinetePredio varchar(255) ,
GabineteSala varchar(255) ,
GabineteTelefone varchar(255) 
);
            ";
            public const string INSERT_DEPUTADO = @"
INSERT INTO deputadoTableName 
(Id,CPF,DataFalecimento,DataNascimento,Escolaridade,MunicipioNascimento,NomeCivil,RedeSocial,Sexo,UFNascimento,URLWebsite,LegislaturaId,Nome,NomeEleitoral,SiglaPartido,SiglaUf,Situacao,PartidoId,URLFoto,CondicaoEleitoral,Data,DescricaoStatus,GabineteAndar,GabineteEmail,GabineteNome,GabinetePredio,GabineteSala,GabineteTelefone) 
VALUES 
(204536,'39313495864',NULL,'1996-01-28 00:00:00','Superior Incompleto','Salto','KIM PATROCA KATAGUIRI','','M','SP',NULL,56,'KIM KATAGUIRI','KIM KATAGUIRI','DEM','SP','Exercício',36769,'https://www.camara.leg.br/internet/deputado/bandep/204536.jpg','Titular','2019-02-01 11:45:00',NULL,'4','dep.kimkataguiri@camara.leg.br','421','4','421','3215-5421'),
(92346,'10655365770',NULL,'1984-07-10 00:00:00','Superior','Rio de Janeiro','EDUARDO NANTES BOLSONARO','','M','RJ',NULL,56,'EDUARDO BOLSONARO','EDUARDO BOLSONARO','PSL','SP','Exercício',36837,'https://www.camara.leg.br/internet/deputado/bandep/92346.jpg','Titular','2019-02-01 11:45:00',NULL,'3','dep.eduardobolsonaro@camara.leg.br','350','4','350','3215-5350');
            ";
            public const string DELETE_DEPUTADO = @"DELETE FROM deputadoTableName";

            public const string TABLE_DESPESA = @"despesaTableName";
            public const string CREATE_DESPESA = @"
CREATE TABLE despesaTableName (
DeputadoId int NOT NULL,
Ano int NOT NULL,
CnpjCpfFornecedor varchar(255) ,
CodDocumento int NOT NULL,
CodLote int NOT NULL,
CodTipoDocumento int ,
DataDocumento date ,
Mes int NOT NULL,
NomeFornecedor varchar(255) ,
NumDocumento varchar(255) ,
NumRessarcimento varchar(255) ,
Parcela int NOT NULL,
TipoDespesa varchar(255) ,
TipoDocumento varchar(255) ,
URLDocumento varchar(255) ,
ValorDocumento decimal(24,8) NOT NULL,
ValorGlosa decimal(24,8) NOT NULL,
ValorLiquido decimal(24,8) NOT NULL
);
            ";
            public const string INSERT_DESPESA = @"
INSERT INTO despesaTableName 
(DeputadoId,Ano,CnpjCpfFornecedor,CodDocumento,CodLote,CodTipoDocumento,DataDocumento,Mes,NomeFornecedor,NumDocumento,NumRessarcimento,Parcela,TipoDespesa,TipoDocumento,URLDocumento,ValorDocumento,ValorGlosa,ValorLiquido) 
VALUES 
(204536,2019,'02575829000148',1641391,0,0,'2019-03-08 00:00:00',3,'Cia Aérea - AVIANCA','Bilhete: 2444321916','0',0,'Emissão Bilhete Aéreo','Nota Fiscal','',1028.49,0,1028.49),(204536,2019,'02575829000148',1641392,0,0,'2019-03-08 00:00:00',3,'Cia Aérea - AVIANCA','Bilhete: 2444321917','0',0,'Emissão Bilhete Aéreo','Nota Fiscal','',529.94,0,529.94),
(92346,2019,'61695227000193',6792046,1578702,0,'2019-03-12 00:00:00',3,'ELETROPAULO METROPOLITANA DE SÃO PAULO S/A','108749565','',0,'MANUTENÇÃO DE ESCRITÓRIO DE APOIO À ATIVIDADE PARLAMENTAR','Nota Fiscal','',1.89,0,1.89),(92346,2019,'61695227000193',6792058,1578702,0,'2019-03-12 00:00:00',3,'ELETROPAULO METROPOLITANA DE SÃO PAULO S/A','108760568','',0,'MANUTENÇÃO DE ESCRITÓRIO DE APOIO À ATIVIDADE PARLAMENTAR','Nota Fiscal','',35.34,0.21,35.13);            
            ";
            public const string DELETE_DESPESA = @"DELETE FROM despesaTableName WHERE ANO = {0} AND MES = {1}";
        }


        
    }
}