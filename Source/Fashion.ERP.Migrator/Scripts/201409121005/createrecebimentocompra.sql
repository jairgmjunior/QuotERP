drop table ordementradacomprapedidocompra
drop table ordementradacompra

--Recebimento da Compra
CREATE TABLE recebimentocompra(
	id bigint NOT NULL,
	numero bigint NOT NULL,	
	data datetime NOT NULL,
	dataalteracao datetime NOT NULL,
	observacao nvarchar(4000) NULL,
	valor float NOT NULL,
	--comprador_id bigint NOT NULL,
	fornecedor_id bigint NOT NULL,
	unidade_id bigint NOT NULL,
	situacaorecebimentocompra nvarchar(400) NOT NULL,
	CONSTRAINT PK_recebimentocompra PRIMARY KEY (id)
)
GO

--ALTER TABLE recebimentocompra ADD CONSTRAINT FK_recebimentocompra_comprador FOREIGN KEY(comprador_id) REFERENCES pessoa (id)
--GO
ALTER TABLE recebimentocompra ADD CONSTRAINT FK_recebimentocompra_fornecedor FOREIGN KEY(fornecedor_id) REFERENCES pessoa (id)
GO
ALTER TABLE recebimentocompra ADD CONSTRAINT FK_recebimentocompra_unidade FOREIGN KEY(unidade_id) REFERENCES pessoa (id)
GO

-- Tabela de relacionamento recebimentocompra e pedidocompra
CREATE TABLE recebimentocomprapedidocompra(	
	pedidocompra_id bigint NOT NULL,
	recebimentocompra_id bigint NOT NULL) 
GO    
	
ALTER TABLE recebimentocomprapedidocompra ADD CONSTRAINT FK_recebimentocomprapedidocompra_recebimentocompra FOREIGN KEY(recebimentocompra_id)
REFERENCES recebimentocompra (id)
GO

ALTER TABLE recebimentocomprapedidocompra ADD CONSTRAINT FK_recebimentocomprapedidocompra_pedidocompra FOREIGN KEY(pedidocompra_id)
REFERENCES pedidocompra (id)
GO

-- Recebimento Compra Item
CREATE TABLE recebimentocompraitem(
	id bigint NOT NULL,
	quantidade float NOT NULL,
	custo float NOT NULL,
	valortotal float NOT NULL,	
	material_id bigint NOT NULL,
	recebimentocompra_id bigint NOT NULL,
	CONSTRAINT PK_recebimentocompraitem PRIMARY KEY (id)
)
GO

ALTER TABLE recebimentocompraitem  ADD  CONSTRAINT fk_conferenciaentradamaterialitem_recebimentocompra
FOREIGN KEY(recebimentocompra_id) REFERENCES recebimentocompra (id)
GO

ALTER TABLE recebimentocompraitem  ADD  CONSTRAINT fk_recebimentocompraitem_material
FOREIGN KEY(material_id) REFERENCES material (id)
GO

ALTER TABLE conferenciaentradamaterial ADD recebimentocompra_id bigint;

ALTER TABLE conferenciaentradamaterial ADD CONSTRAINT FK_conferenciaentradamaterial_recebimentocompra FOREIGN KEY(recebimentocompra_id)
REFERENCES recebimentocompra (id)
GO

-- Tabela de DetalhamentoRecebimentoCompraItem
CREATE TABLE detalhamentorecebimentocompraitem (	
	quantidade float NOT NULL,
	pedidocompraitem_id bigint NOT NULL,
	pedidocompra_id bigint NOT NULL,
	recebimentocompra_id bigint NOT NULL)
GO    
	
ALTER TABLE detalhamentorecebimentocompraitem ADD CONSTRAINT FK_detalhamentorecebimentocompraitem_pedidocompra FOREIGN KEY(pedidocompra_id)
REFERENCES pedidocompra (id)
GO

ALTER TABLE detalhamentorecebimentocompraitem ADD CONSTRAINT FK_detalhamentorecebimentocompraitem_recebimentocompra FOREIGN KEY(recebimentocompra_id)
REFERENCES recebimentocompra (id)
GO

ALTER TABLE detalhamentorecebimentocompraitem ADD CONSTRAINT FK_detalhamentorecebimentocompraitem_pedidocompraitem FOREIGN KEY(pedidocompraitem_id)
REFERENCES pedidocompraitem (id)
GO