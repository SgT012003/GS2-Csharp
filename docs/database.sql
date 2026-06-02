-- ============================================================
--  GLOBAL SOLUTION 2025 - Nova Economia Espacial
--  Script de criação do banco de dados
--  Banco: MySQL 8.0
--  Gerado com base nas Migrations do Entity Framework Core
-- ============================================================

-- Cria o banco de dados caso não exista e o seleciona
CREATE DATABASE IF NOT EXISTS gs_db
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE gs_db;

-- ============================================================
--  TABELA: Categorias
--  Representa as categorias de impacto das tecnologias
--  (ex: Saúde Global, Sustentabilidade, etc.)
-- ============================================================
CREATE TABLE IF NOT EXISTS `Categorias` (
    `Id`       INT          NOT NULL AUTO_INCREMENT,
    `Nome`     LONGTEXT     NOT NULL,
    `Descricao` LONGTEXT    NOT NULL,
    CONSTRAINT `PK_Categorias` PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ============================================================
--  TABELA: Usuarios
--  Armazena os usuários do sistema (Administrador / Pesquisador)
-- ============================================================
CREATE TABLE IF NOT EXISTS `Usuarios` (
    `Id`        INT          NOT NULL AUTO_INCREMENT,
    `Nome`      LONGTEXT     NOT NULL,
    `Email`     LONGTEXT     NOT NULL,
    `SenhaHash` LONGTEXT     NOT NULL,
    `Perfil`    LONGTEXT     NOT NULL,
    CONSTRAINT `PK_Usuarios` PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ============================================================
--  TABELA: Origens
--  Representa a origem espacial da tecnologia
--  (ex: Missão Apollo, ISS, Satélites, etc.)
-- ============================================================
CREATE TABLE IF NOT EXISTS `Origens` (
    `Id`       INT          NOT NULL AUTO_INCREMENT,
    `Nome`     LONGTEXT     NOT NULL,
    `Descricao` LONGTEXT    NOT NULL,
    CONSTRAINT `PK_Origens` PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ============================================================
--  TABELA: Tecnologias
--  Tecnologias espaciais adaptadas para uso na Terra
--  FK -> Categorias (CategoriaImpactoId)
--  FK -> Origens    (OrigemId)
-- ============================================================
CREATE TABLE IF NOT EXISTS `Tecnologias` (
    `Id`                INT          NOT NULL AUTO_INCREMENT,
    `Nome`              LONGTEXT     NOT NULL,
    `Descricao`         LONGTEXT     NOT NULL,
    `DataCadastro`      DATETIME(6)  NOT NULL,
    `CategoriaImpactoId` INT         NOT NULL,
    `OrigemId`          INT          NOT NULL,
    CONSTRAINT `PK_Tecnologias` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Tecnologias_Categorias_CategoriaImpactoId`
        FOREIGN KEY (`CategoriaImpactoId`) REFERENCES `Categorias` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_Tecnologias_Origens_OrigemId`
        FOREIGN KEY (`OrigemId`) REFERENCES `Origens` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Índices de chave estrangeira para otimizar JOINs
CREATE INDEX IF NOT EXISTS `IX_Tecnologias_CategoriaImpactoId`
    ON `Tecnologias` (`CategoriaImpactoId`);

CREATE INDEX IF NOT EXISTS `IX_Tecnologias_OrigemId`
    ON `Tecnologias` (`OrigemId`);

-- ============================================================
--  DADOS INICIAIS (SEED)
--  Espelha o que o DatabaseSeeder insere automaticamente
--  na primeira execução da aplicação
-- ============================================================

-- Usuário administrador padrão
-- Senha: Admin@123  (armazenada como hash BCrypt pela aplicação)
INSERT INTO `Usuarios` (`Nome`, `Email`, `SenhaHash`, `Perfil`)
SELECT 'Administrador Chefe', 'admin@novaeconomia.space', '<HASH_GERADO_PELA_APLICACAO>', 'Administrador'
WHERE NOT EXISTS (SELECT 1 FROM `Usuarios` LIMIT 1);

-- Categorias de impacto
INSERT INTO `Categorias` (`Nome`, `Descricao`)
SELECT * FROM (
    SELECT 'Saúde Global'          AS Nome, 'Tecnologias que impactam tratamentos e diagnósticos'      AS Descricao UNION ALL
    SELECT 'Agricultura e Clima',           'Monitoramento e melhoria de safras'                        UNION ALL
    SELECT 'Comunicações',                  'Melhoria na transmissão de dados e internet'               UNION ALL
    SELECT 'Sustentabilidade',              'Gerenciamento de recursos naturais e energia'
) AS dados
WHERE NOT EXISTS (SELECT 1 FROM `Categorias` LIMIT 1);

-- Origens espaciais
INSERT INTO `Origens` (`Nome`, `Descricao`)
SELECT * FROM (
    SELECT 'Missão Apollo'                     AS Nome, 'Programa espacial americano que levou o homem à Lua.'                   AS Descricao UNION ALL
    SELECT 'Estação Espacial Internacional (ISS)',      'Laboratório espacial orbital colaborativo.'                              UNION ALL
    SELECT 'Satélites de Observação',                  'Satélites em órbita da Terra usados para monitoramento.'                UNION ALL
    SELECT 'Missão Artemis',                           'Nova missão de exploração lunar da NASA.'                               UNION ALL
    SELECT 'Sondas Interplanetárias',                  'Naves não tripuladas enviadas para explorar o sistema solar.'           UNION ALL
    SELECT 'Outros',                                   'Outras origens espaciais.'
) AS dados
WHERE NOT EXISTS (SELECT 1 FROM `Origens` LIMIT 1);

-- Tecnologias de exemplo
INSERT INTO `Tecnologias` (`Nome`, `Descricao`, `DataCadastro`, `CategoriaImpactoId`, `OrigemId`)
SELECT * FROM (
    SELECT
        'Termômetros Infravermelhos'                                                           AS Nome,
        'Desenvolvidos inicialmente para medir a temperatura de estrelas.'                     AS Descricao,
        NOW() - INTERVAL 10 DAY                                                               AS DataCadastro,
        (SELECT `Id` FROM `Categorias` WHERE `Nome` = 'Saúde Global'          LIMIT 1)        AS CategoriaImpactoId,
        (SELECT `Id` FROM `Origens`    WHERE `Nome` = 'Missão Apollo'         LIMIT 1)        AS OrigemId
    UNION ALL
    SELECT
        'Purificador de Água',
        'Sistema de filtragem criado para reciclar água em missões espaciais.',
        NOW() - INTERVAL 5 DAY,
        (SELECT `Id` FROM `Categorias` WHERE `Nome` = 'Sustentabilidade'                        LIMIT 1),
        (SELECT `Id` FROM `Origens`    WHERE `Nome` = 'Estação Espacial Internacional (ISS)'   LIMIT 1)
    UNION ALL
    SELECT
        'Sensores CMOS (Câmeras)',
        'Câmeras miniaturizadas inicialmente para sondas espaciais.',
        NOW() - INTERVAL 2 DAY,
        (SELECT `Id` FROM `Categorias` WHERE `Nome` = 'Comunicações'             LIMIT 1),
        (SELECT `Id` FROM `Origens`    WHERE `Nome` = 'Satélites de Observação'  LIMIT 1)
) AS dados
WHERE NOT EXISTS (SELECT 1 FROM `Tecnologias` LIMIT 1);

-- ============================================================
--  FIM DO SCRIPT
-- ============================================================
