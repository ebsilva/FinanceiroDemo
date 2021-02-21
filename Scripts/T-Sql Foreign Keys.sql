EXEC sp_RENAME 'Propostas.situacao' , 'situacaoid', 'COLUMN'

ALTER TABLE ContaPrs  DROP CONSTRAINT  FK_ContaPrs_Clientes_ID;
ALTER TABLE ContaPrs  ADD CONSTRAINT   FK_ContaPrs_Clientes_ID  FOREIGN KEY (cliforid)   REFERENCES Clientes(clienteid);

ALTER TABLE Propostas DROP CONSTRAINT  FK_Propostas_Clientes_ID;
ALTER TABLE Propostas ADD CONSTRAINT   FK_Propostas_Clientes_ID FOREIGN KEY  (clienteid)  REFERENCES Clientes(clienteid);

ALTER TABLE ContatoeS DROP CONSTRAINT  FK_Contatos_Clientes_ID;
ALTER TABLE Contatoes ADD CONSTRAINT   FK_Contatos_Clientes_ID  FOREIGN KEY  (clienteid)  REFERENCES Clientes(clienteid);


ALTER TABLE Clientes  DROP CONSTRAINT  FK_Clientes_Cidades_ID; 
ALTER TABLE Clientes  ADD CONSTRAINT   FK_Clientes_Cidades_ID   FOREIGN KEY (cidadeid)   REFERENCES Cidades(cidadeid);

--ALTER TABLE Propostas DROP CONSTRAINT  FK_Propostas_Situacao_ID;
--ALTER TABLE Propostas ADD CONSTRAINT  FK_Propostas_Situacao_ID FOREIGN KEY  (situacaoid) REFERENCES Situacaos(situacaoid);