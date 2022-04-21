-- ** 26/05/2021
ALTER TABLE `t_piecejointe` ADD `ID_Pieces` BIGINT(19) NOT NULL FIRST;
ALTER TABLE `t_piecejointe` ADD PRIMARY KEY( `ID_Pieces`);
ALTER TABLE `t_piecejointe` CHANGE `ID_Pieces` `ID_Pieces` BIGINT(19) NOT NULL AUTO_INCREMENT;
ALTER TABLE `t_rapportactivites` CHANGE `pourcentagetotal` `pourcentagetotal` DOUBLE(3,0) NOT NULL;

-- ** 31/05/2021
ALTER TABLE `t_rapportactivites` CHANGE `DateModif` `DateAjout` VARCHAR(100) CHARACTER SET latin1 COLLATE latin1_swedish_ci NOT NULL;
ALTER TABLE `t_rapportactivites` ADD `DtDebRapport` DATETIME NOT NULL AFTER `TypeRapport`, ADD `DtFinRapport` DATETIME NOT NULL AFTER `DtDebRapport`;
ALTER TABLE `t_rapportactivites` DROP `PeriodeRapport`;