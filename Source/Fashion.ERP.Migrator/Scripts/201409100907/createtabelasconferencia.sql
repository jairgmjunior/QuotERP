ALTER TABLE ordementradacompra drop constraint fk_ordementradacompra_ordementradamaterial
drop table ordementradaitemmaterial
drop table ordementradamaterial

CREATE TABLE ConferenciaEntradaMaterial(
	id bigint NOT NULL,
	numero bigint NOT NULL,	
	data datetime NOT NULL,
	dataatualizacao datetime NOT NULL,
	observacao nvarchar(4000) NULL,
	comprador_id bigint NOT NULL,
	autorizado bit NOT NULL,
	conferido bit NOT NULL,
	CONSTRAINT PK_conferenicaentradamaterial PRIMARY KEY (id)
)
GO
ALTER TABLE conferenciaentradamaterial ADD  CONSTRAINT FK_conferenicaentradamaterial_comprador FOREIGN KEY(comprador_id) REFERENCES pessoa (id)
GO

CREATE TABLE conferenciaentradamaterialitem(
	id bigint NOT NULL,
	quantidade float NOT NULL,
	quantidadeconferida float NOT NULL,
	situacaoconferencia nvarchar (254) NOT NULL,
	unidademedida_id bigint NOT NULL,
	material_id bigint NOT NULL,
	conferenciaentradamaterial_id bigint NOT NULL,

CONSTRAINT PK_conferenciaentradamaterialitem PRIMARY KEY (id)
)
GO

ALTER TABLE conferenciaentradamaterialitem  ADD  CONSTRAINT fk_conferenciaentradamaterialitem_conferenciaentradamaterial
FOREIGN KEY(conferenciaentradamaterial_id) REFERENCES conferenciaentradamaterial (id)
GO

ALTER TABLE conferenciaentradamaterialitem  ADD  CONSTRAINT fk_conferenciaentradamaterialitem_material
FOREIGN KEY(material_id) REFERENCES material (id)
GO

ALTER TABLE conferenciaentradamaterialitem  ADD  CONSTRAINT fk_conferenciaentradamaterialitem_unidademedida
FOREIGN KEY(unidademedida_id) REFERENCES unidademedida (id)
GO