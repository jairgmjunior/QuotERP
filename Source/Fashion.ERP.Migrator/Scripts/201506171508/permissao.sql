DECLARE @EDITARID AS BIGINT;
set @EDITARID = (select id from  [dbo].[permissao] where descricao = 'Editar' and action = 'Editar' and area = 'Producao' and controller = 'FichaTecnica')

INSERT INTO [dbo].[permissao]
           ([descricao]
           ,[action]
           ,[area]
           ,[controller]
           ,[exibenomenu]
           ,[requerpermissao]
           ,[permissaopai_id]
           ,[ordem])
     VALUES
           ('Modelagem'
           ,'Modelagem'
           ,'Producao'
           ,'FichaTecnica'
           ,0
           ,1
           ,@EDITARID
           ,0
		   ) 
		   
DECLARE @PRODUCAOID AS BIGINT, @RELATORIOID AS BIGINT, @ID AS BIGINT;
SET @PRODUCAOID = (select id from permissao where area = 'Producao' and descricao = 'Produção');

INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) 
VALUES (NULL, 'Producao', null, 'Relatórios',1 ,0, 0, @PRODUCAOID);		    
SET @RELATORIOID = SCOPE_IDENTITY();

INSERT INTO permissao (Action, Area, Controller, Descricao, ExibeNoMenu, RequerPermissao, Ordem, permissaopai_id) 
VALUES ('EstimativaConsumoProgramado', 'Producao', 'RelatorioEstimativaConsumoProgramado', 'Estimativa de Consumo Programado',1 ,1,0, @RELATORIOID);		    

DECLARE @SOLICITACAOID AS BIGINT;
SET @SOLICITACAOID = (select id from permissao where action= 'SolicitacaoMaterialCompra' and area = 'EngenhariaProduto' and controller = 'RelatorioSolicitacaoMaterialCompra');
DELETE FROM permissaotousuario WHERE permissao_id = @SOLICITACAOID;
DELETE FROM permissaotoperfildeacesso WHERE permissao_id = @SOLICITACAOID;
DELETE FROM permissao WHERE ID = @SOLICITACAOID;