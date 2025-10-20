/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 90200 (9.2.0)
 Source Host           : localhost:3307
 Source Schema         : diars

 Target Server Type    : MySQL
 Target Server Version : 90200 (9.2.0)
 File Encoding         : 65001

 Date: 20/10/2025 00:33:35
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for bus
-- ----------------------------
DROP TABLE IF EXISTS `bus`;
CREATE TABLE `bus`  (
  `BusB` int NOT NULL AUTO_INCREMENT,
  `Marca` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Modelo` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PisoBus` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `NPlaca` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `NChasis` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `NMotor` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Capacidad` int NOT NULL,
  `TipoMotor` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Combustible` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `FechaAdquisicion` datetime NOT NULL,
  `Kilometraje` int NOT NULL,
  `EstadoB` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`BusB`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 14 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of bus
-- ----------------------------
BEGIN;
INSERT INTO `bus` (`BusB`, `Marca`, `Modelo`, `PisoBus`, `NPlaca`, `NChasis`, `NMotor`, `Capacidad`, `TipoMotor`, `Combustible`, `FechaAdquisicion`, `Kilometraje`, `EstadoB`) VALUES (1, 'Mercedes-Benz', 'Sprinter', '2 Pisos', 'ABC123', 'CH123456789', 'MN987654321', 60, 'Diesel', 'Diesel', '2020-04-10 00:00:00', 2800, 0), (2, 'Volvo', 'B-9400', '2 pisos', 'CBN840', 'DH123456789', 'TU987654321', 70, 'Hibrido', 'Gasolina', '2021-05-09 21:14:13', 2500, 1), (3, 'Scania', 'K440', '2 Pisos', 'LMN-2345', 'SCN987654321ABCD', 'DC13 147', 65, 'Diésel Euro V', 'Diésel', '2021-05-09 00:00:00', 92000, 1), (4, 'Marcopolo', 'Scania Touring', '2 Pisos', 'TBU-743', '9BM384095GB001245', 'OM457LA123456789', 60, 'Motor Diésel', 'Diésel', '2022-05-14 00:00:00', 1500, 1), (5, 'Mercedes-Benz', 'Mercedes-Benz OH 1621', '1 Piso', 'DEF-5678', 'CHS456789123', 'MTR321654987', 45, 'Motor Diesel', 'Diésel', '2019-11-05 00:00:00', 90000, 1), (6, 'Volvo', 'Volvo B11R', '2 Pisos', 'GHI-2345', 'CHS654321987', 'MTR789123456', 35, 'Motor Diesel', 'Diésel', '2021-05-18 00:00:00', 60000, 1), (7, 'Mercedes-Benz', 'Mercedes-Benz O500U', '1 Piso', 'JKL-6789', 'CHS789456123', 'MTR456789123', 30, 'Motor Gasolina', 'Gasolina', '2022-09-12 00:00:00', 30000, 1), (8, 'Volvo', 'Volvo 9700', '2 Pisos', 'MNO-3456', 'CHS321987654', 'MTR123789456', 55, 'Motor Diesel', 'Diésel', '2018-12-01 00:00:00', 140000, 1), (9, 'Scania', 'Scania K 360', '1 Piso', 'PQR-7890', 'CHS987123654', 'MTR654987321', 48, 'Motor Diesel', 'Diésel', '2020-03-25 00:00:00', 80000, 1), (10, 'Volvo', 'Volvo 9700', '2 Pisos', 'STU-234', 'CHS112233445', 'MTR112233445', 65, 'Motor Gasolina', 'Gasolina', '2025-04-17 00:00:00', 1200, 1), (11, 'Mercedes-Benz', 'Mercedes-Benz OH 1621', '1 Piso', 'THO-897', 'CHS556677889', 'MTR556677889', 60, 'Motor Gasolina', 'Gasolina', '2025-04-09 00:00:00', 1200, 1), (12, 'Mercedes-Benz', 'Mercedes-Benz LO 915', '1 Piso', 'DEF-456', 'WDB001ABCDE1', 'ME123456789', 50, 'Motor Diésel', 'Diésel', '2025-06-19 00:00:00', 150000, 0), (13, 'Volvo', 'B8R', 'Doble', 'ABC-123', 'B8R-400', 'D8K', 50, 'Diesel', 'ACPM', '2023-10-17 00:00:00', 10000, 1);
COMMIT;

-- ----------------------------
-- Table structure for categoria
-- ----------------------------
DROP TABLE IF EXISTS `categoria`;
CREATE TABLE `categoria`  (
  `CodigoC` int NOT NULL AUTO_INCREMENT,
  `NombreC` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Descripcion` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `EstadoC` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`CodigoC`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 16 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of categoria
-- ----------------------------
BEGIN;
INSERT INTO `categoria` (`CodigoC`, `NombreC`, `Descripcion`, `EstadoC`) VALUES (1, 'Motor', 'Repuestos del sistema de motor', 1), (2, 'Frenos', 'Componentes del sistema de frenos', 1), (3, 'Suspensión', 'Partes del sistema de suspensión', 1), (4, 'Electricidad', 'Sistemas y cableado eléctrico', 1), (5, 'Combustible', 'Componentes del sistema de combustible', 1), (6, 'Llantas y Rines', 'Neumáticos y rines', 1), (7, 'Climatización', 'Aire acondicionado y calefacción', 1), (8, 'Motor', 'Repuestos del sistema de motor.', 1), (9, 'Frenos', 'Componentes del sistema de frenos', 1), (10, 'Suspensión', 'Partes del sistema de suspensión', 1), (11, 'Electricidad', 'Sistemas y cableado eléctrico', 1), (12, 'Combustible', 'Componentes del sistema de combustible.', 1), (13, 'Llantas y Rines', 'Neumáticos y rines', 1), (14, 'Climatización', 'Aire acondicionado y calefacción', 1), (15, 'Refrigeracion', 'repuestos de refrigeracion', 1);
COMMIT;

-- ----------------------------
-- Table structure for contratomantenimiento
-- ----------------------------
DROP TABLE IF EXISTS `contratomantenimiento`;
CREATE TABLE `contratomantenimiento`  (
  `CodigoCM` int NOT NULL AUTO_INCREMENT,
  `BusCM` int NOT NULL,
  `Fecha` datetime NOT NULL,
  `ProveedorCM` int NOT NULL,
  `Descripcion` varchar(80) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Costo` decimal(18, 2) NOT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`CodigoCM`) USING BTREE,
  INDEX `BusCM`(`BusCM` ASC) USING BTREE,
  INDEX `ProveedorCM`(`ProveedorCM` ASC) USING BTREE,
  CONSTRAINT `contratomantenimiento_ibfk_1` FOREIGN KEY (`BusCM`) REFERENCES `bus` (`BusB`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `contratomantenimiento_ibfk_2` FOREIGN KEY (`ProveedorCM`) REFERENCES `proveedor` (`CodigoP`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 8 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of contratomantenimiento
-- ----------------------------
BEGIN;
INSERT INTO `contratomantenimiento` (`CodigoCM`, `BusCM`, `Fecha`, `ProveedorCM`, `Descripcion`, `Costo`, `Estado`) VALUES (5, 1, '2024-04-12 00:00:00', 1, 'Mantenimiento preventivo de motor', 1500.00, 1), (6, 2, '2024-06-07 00:00:00', 2, 'Revisión y cambio de frenos', 850.50, 0), (7, 3, '2025-06-10 00:00:00', 2, 'Reparación de buses por choque.', 2800.00, 1);
COMMIT;

-- ----------------------------
-- Table structure for detalleevaluacioninterna
-- ----------------------------
DROP TABLE IF EXISTS `detalleevaluacioninterna`;
CREATE TABLE `detalleevaluacioninterna`  (
  `DetalleEvaluacionInternaID` int NOT NULL AUTO_INCREMENT,
  `EICodigo` int NOT NULL,
  `MecanicoEI` int NOT NULL,
  PRIMARY KEY (`DetalleEvaluacionInternaID`) USING BTREE,
  INDEX `EICodigo`(`EICodigo` ASC) USING BTREE,
  INDEX `MecanicoEI`(`MecanicoEI` ASC) USING BTREE,
  CONSTRAINT `detalleevaluacioninterna_ibfk_1` FOREIGN KEY (`EICodigo`) REFERENCES `evaluacioninterna` (`CodigoEI`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `detalleevaluacioninterna_ibfk_2` FOREIGN KEY (`MecanicoEI`) REFERENCES `mecanico` (`CodigoM`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 10 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of detalleevaluacioninterna
-- ----------------------------
BEGIN;
INSERT INTO `detalleevaluacioninterna` (`DetalleEvaluacionInternaID`, `EICodigo`, `MecanicoEI`) VALUES (1, 1, 1), (2, 2, 2), (3, 3, 4), (4, 4, 1), (5, 4, 3), (6, 5, 2), (7, 5, 3), (8, 6, 1), (9, 6, 2);
COMMIT;

-- ----------------------------
-- Table structure for detallenotaingreso
-- ----------------------------
DROP TABLE IF EXISTS `detallenotaingreso`;
CREATE TABLE `detallenotaingreso`  (
  `DetalleNotaIngresoID` int NOT NULL AUTO_INCREMENT,
  `IRCodigo` int NOT NULL,
  `CantidadRecibida` int NOT NULL,
  `CodigoRepu` int NOT NULL,
  `CantidadAceptada` int NOT NULL,
  `Precio` decimal(18, 2) NOT NULL,
  PRIMARY KEY (`DetalleNotaIngresoID`) USING BTREE,
  INDEX `IRCodigo`(`IRCodigo` ASC) USING BTREE,
  INDEX `CodigoRepu`(`CodigoRepu` ASC) USING BTREE,
  CONSTRAINT `detallenotaingreso_ibfk_1` FOREIGN KEY (`IRCodigo`) REFERENCES `notaingresorepuestos` (`CodigoIR`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `detallenotaingreso_ibfk_2` FOREIGN KEY (`CodigoRepu`) REFERENCES `repuesto` (`CodigoR`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 11 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of detallenotaingreso
-- ----------------------------
BEGIN;
INSERT INTO `detallenotaingreso` (`DetalleNotaIngresoID`, `IRCodigo`, `CantidadRecibida`, `CodigoRepu`, `CantidadAceptada`, `Precio`) VALUES (1, 1, 50, 1, 50, 25.50), (2, 2, 30, 2, 30, 40.00), (3, 3, 20, 5, 18, 60.70), (4, 4, 2, 2, 2, 22.50), (5, 4, 1, 4, 1, 15.00), (6, 5, 3, 1, 3, 75.00), (7, 5, 1, 4, 1, 15.00), (8, 5, 2, 6, 2, 50.00), (9, 6, 1, 1, 1, 75.00), (10, 6, 1, 3, 1, 240.00);
COMMIT;

-- ----------------------------
-- Table structure for detallenotasalida
-- ----------------------------
DROP TABLE IF EXISTS `detallenotasalida`;
CREATE TABLE `detallenotasalida`  (
  `DetalleNotaSalidaID` int NOT NULL AUTO_INCREMENT,
  `DSRCodigo` int NOT NULL,
  `CantidadRecibida` int NOT NULL,
  `CodigoRepu` int NOT NULL,
  `CantidadEnviada` int NOT NULL,
  PRIMARY KEY (`DetalleNotaSalidaID`) USING BTREE,
  INDEX `DSRCodigo`(`DSRCodigo` ASC) USING BTREE,
  INDEX `CodigoRepu`(`CodigoRepu` ASC) USING BTREE,
  CONSTRAINT `detallenotasalida_ibfk_1` FOREIGN KEY (`DSRCodigo`) REFERENCES `notasalidarepuesto` (`CodigoSR`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `detallenotasalida_ibfk_2` FOREIGN KEY (`CodigoRepu`) REFERENCES `repuesto` (`CodigoR`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 8 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of detallenotasalida
-- ----------------------------
BEGIN;
INSERT INTO `detallenotasalida` (`DetalleNotaSalidaID`, `DSRCodigo`, `CantidadRecibida`, `CodigoRepu`, `CantidadEnviada`) VALUES (1, 1, 40, 1, 40), (2, 2, 25, 2, 25), (3, 3, 15, 5, 15), (4, 4, 1, 1, 1), (5, 4, 3, 2, 3), (6, 4, 1, 4, 1), (7, 4, 1, 5, 1);
COMMIT;

-- ----------------------------
-- Table structure for detalleordencompra
-- ----------------------------
DROP TABLE IF EXISTS `detalleordencompra`;
CREATE TABLE `detalleordencompra`  (
  `DetalleOrdenCompraID` int NOT NULL AUTO_INCREMENT,
  `OCCompra` int NOT NULL,
  `Cantidad` int NOT NULL,
  `CodigoRep` int NOT NULL,
  `Precio` decimal(18, 2) NOT NULL,
  `CantidadAceptada` int NULL DEFAULT 0,
  PRIMARY KEY (`DetalleOrdenCompraID`) USING BTREE,
  INDEX `OCCompra`(`OCCompra` ASC) USING BTREE,
  INDEX `CodigoRep`(`CodigoRep` ASC) USING BTREE,
  CONSTRAINT `detalleordencompra_ibfk_2` FOREIGN KEY (`CodigoRep`) REFERENCES `repuesto` (`CodigoR`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 19 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of detalleordencompra
-- ----------------------------
BEGIN;
INSERT INTO `detalleordencompra` (`DetalleOrdenCompraID`, `OCCompra`, `Cantidad`, `CodigoRep`, `Precio`, `CantidadAceptada`) VALUES (4, 4, 2, 1, 75.00, 0), (5, 5, 4, 2, 22.50, 0), (6, 4, 10, 5, 10.00, 0), (7, 1, 20, 1, 25.50, 0), (8, 2, 15, 2, 40.00, 0), (9, 3, 10, 3, 60.70, 0), (10, 4, 1, 1, 75.00, 1), (11, 4, 3, 2, 22.50, 2), (12, 4, 1, 4, 15.00, 1), (13, 4, 1, 5, 80.00, 1), (14, 5, 3, 1, 75.00, 3), (15, 5, 1, 4, 15.00, 1), (16, 5, 2, 6, 50.00, 2), (17, 6, 1, 1, 75.00, 1), (18, 6, 1, 3, 240.00, 1);
COMMIT;

-- ----------------------------
-- Table structure for detalleordenpedido
-- ----------------------------
DROP TABLE IF EXISTS `detalleordenpedido`;
CREATE TABLE `detalleordenpedido`  (
  `DetalleOrdenPedidoID` int NOT NULL AUTO_INCREMENT,
  `OPCodigo` int NOT NULL,
  `Cantidad` int NOT NULL,
  `CodigoRepu` int NOT NULL,
  PRIMARY KEY (`DetalleOrdenPedidoID`) USING BTREE,
  INDEX `OPCodigo`(`OPCodigo` ASC) USING BTREE,
  INDEX `CodigoRepu`(`CodigoRepu` ASC) USING BTREE,
  CONSTRAINT `detalleordenpedido_ibfk_1` FOREIGN KEY (`OPCodigo`) REFERENCES `ordenpedido` (`CodigoOP`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `detalleordenpedido_ibfk_2` FOREIGN KEY (`CodigoRepu`) REFERENCES `repuesto` (`CodigoR`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 13 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of detalleordenpedido
-- ----------------------------
BEGIN;
INSERT INTO `detalleordenpedido` (`DetalleOrdenPedidoID`, `OPCodigo`, `Cantidad`, `CodigoRepu`) VALUES (1, 1, 10, 1), (2, 2, 5, 2), (3, 3, 8, 3), (4, 4, 1, 1), (5, 4, 3, 2), (6, 4, 1, 4), (7, 4, 1, 5), (8, 5, 3, 1), (9, 5, 1, 4), (10, 5, 2, 6), (11, 6, 1, 1), (12, 6, 1, 3);
COMMIT;

-- ----------------------------
-- Table structure for detalleote
-- ----------------------------
DROP TABLE IF EXISTS `detalleote`;
CREATE TABLE `detalleote`  (
  `DetalleOTEID` int NOT NULL AUTO_INCREMENT,
  `TECodigo` int NOT NULL,
  `CodigoRepu` int NOT NULL,
  `Parte` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Pieza` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Cantidad` int NOT NULL,
  PRIMARY KEY (`DetalleOTEID`) USING BTREE,
  INDEX `TECodigo`(`TECodigo` ASC) USING BTREE,
  INDEX `CodigoRepu`(`CodigoRepu` ASC) USING BTREE,
  CONSTRAINT `detalleote_ibfk_1` FOREIGN KEY (`TECodigo`) REFERENCES `ordentrabajoexterno` (`CodigoTE`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `detalleote_ibfk_2` FOREIGN KEY (`CodigoRepu`) REFERENCES `repuesto` (`CodigoR`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of detalleote
-- ----------------------------
BEGIN;
INSERT INTO `detalleote` (`DetalleOTEID`, `TECodigo`, `CodigoRepu`, `Parte`, `Pieza`, `Cantidad`) VALUES (1, 1, 1, 'Motor', 'Bujía', 4), (2, 2, 2, 'Freno', 'Pastilla', 8), (3, 3, 3, 'Transmisión', 'Engranaje', 2), (4, 4, 1, 'Motor', 'Inyeccion de Combustible', 2), (5, 4, 3, 'Frenos', 'Caja de Cambios', 3), (6, 4, 5, 'Frenos', 'Caja de Cambios', 1);
COMMIT;

-- ----------------------------
-- Table structure for detalleoti
-- ----------------------------
DROP TABLE IF EXISTS `detalleoti`;
CREATE TABLE `detalleoti`  (
  `DetalleOTIID` int NOT NULL AUTO_INCREMENT,
  `OrdenTrabajoInternoID` int NOT NULL,
  `CodigoRepu` int NOT NULL,
  `MecanicoTI` int NOT NULL,
  `Parte` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Pieza` varchar(40) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Cantidad` int NOT NULL,
  PRIMARY KEY (`DetalleOTIID`) USING BTREE,
  INDEX `OrdenTrabajoInternoID`(`OrdenTrabajoInternoID` ASC) USING BTREE,
  INDEX `CodigoRepu`(`CodigoRepu` ASC) USING BTREE,
  INDEX `MecanicoTI`(`MecanicoTI` ASC) USING BTREE,
  CONSTRAINT `detalleoti_ibfk_1` FOREIGN KEY (`OrdenTrabajoInternoID`) REFERENCES `ordentrabajointerno` (`CodigoTI`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `detalleoti_ibfk_2` FOREIGN KEY (`CodigoRepu`) REFERENCES `repuesto` (`CodigoR`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `detalleoti_ibfk_3` FOREIGN KEY (`MecanicoTI`) REFERENCES `mecanico` (`CodigoM`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 24 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of detalleoti
-- ----------------------------
BEGIN;
INSERT INTO `detalleoti` (`DetalleOTIID`, `OrdenTrabajoInternoID`, `CodigoRepu`, `MecanicoTI`, `Parte`, `Pieza`, `Cantidad`) VALUES (1, 1, 1, 1, 'Motor', 'Bujía', 4), (2, 2, 2, 3, 'Freno', 'Pastilla', 8), (3, 3, 3, 5, 'Transmisión', 'Engranaje', 2), (4, 4, 1, 1, 'Motor', 'Caja de Cambios', 1), (5, 4, 2, 3, 'Transmicion', 'Embrague', 3), (6, 4, 4, 3, 'Suspencion', 'Resortes', 1), (7, 4, 5, 3, 'Suspencion', 'Resortes', 1), (8, 5, 1, 2, 'Motor', 'Inyeccion de Combustible', 3), (9, 5, 4, 3, 'Suspencion', 'Caja de Cambios', 1), (10, 5, 6, 3, 'Suspencion', 'Caja de Cambios', 2), (11, 6, 1, 1, 'Motor', 'Inyeccion de Combustible', 1), (12, 6, 3, 2, 'Suspencion', 'Embrague', 2), (13, 6, 4, 2, 'Suspencion', 'Embrague', 1), (14, 7, 2, 1, 'Motor', 'Inyeccion de Combustible', 2), (15, 7, 3, 2, 'Suspencion', 'Resortes', 1), (16, 7, 5, 4, 'Suspencion', 'Resortes', 1), (17, 8, 1, 4, 'Motor', 'Pistones', 1), (18, 8, 2, 2, 'Transmicion', 'Embrague', 1), (19, 8, 3, 2, 'Transmicion', 'Embrague', 2), (20, 8, 5, 2, 'Transmicion', 'Embrague', 1), (21, 9, 2, 2, 'Transmicion', 'Embrague', 2), (22, 9, 4, 3, 'Transmicion', 'Caja de Cambios', 2), (23, 9, 6, 3, 'Transmicion', 'Caja de Cambios', 1);
COMMIT;

-- ----------------------------
-- Table structure for especialidad
-- ----------------------------
DROP TABLE IF EXISTS `especialidad`;
CREATE TABLE `especialidad`  (
  `CodigoS` int NOT NULL AUTO_INCREMENT,
  `NombreS` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Descripcion` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `EstadoE` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`CodigoS`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 5 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of especialidad
-- ----------------------------
BEGIN;
INSERT INTO `especialidad` (`CodigoS`, `NombreS`, `Descripcion`, `EstadoE`) VALUES (1, 'Electricista', 'Mantenimiento de sistemas eléctricos', 1), (2, 'Mecánico General', 'Mantenimiento general de buses', 1), (3, 'Frenos', 'Especialista en sistemas de frenos', 1), (4, 'Neumatico', 'Arregla las llantas', 1);
COMMIT;

-- ----------------------------
-- Table structure for evaluacionexterna
-- ----------------------------
DROP TABLE IF EXISTS `evaluacionexterna`;
CREATE TABLE `evaluacionexterna`  (
  `CodigoEE` int NOT NULL AUTO_INCREMENT,
  `CodigoBus` int NOT NULL,
  `ProveedorEE` int NOT NULL,
  `Fecha` datetime NOT NULL,
  `TECodigo` int NOT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`CodigoEE`) USING BTREE,
  INDEX `CodigoBus`(`CodigoBus` ASC) USING BTREE,
  INDEX `ProveedorEE`(`ProveedorEE` ASC) USING BTREE,
  INDEX `TECodigo`(`TECodigo` ASC) USING BTREE,
  CONSTRAINT `evaluacionexterna_ibfk_1` FOREIGN KEY (`CodigoBus`) REFERENCES `bus` (`BusB`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `evaluacionexterna_ibfk_2` FOREIGN KEY (`ProveedorEE`) REFERENCES `proveedor` (`CodigoP`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `evaluacionexterna_ibfk_3` FOREIGN KEY (`TECodigo`) REFERENCES `ordentrabajoexterno` (`CodigoTE`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 5 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of evaluacionexterna
-- ----------------------------
BEGIN;
INSERT INTO `evaluacionexterna` (`CodigoEE`, `CodigoBus`, `ProveedorEE`, `Fecha`, `TECodigo`, `Estado`) VALUES (1, 1, 1, '2025-06-14 00:00:00', 1, 1), (2, 2, 2, '2025-06-14 00:00:00', 2, 1), (3, 1, 2, '2025-06-14 00:00:00', 3, 1), (4, 3, 2, '2025-06-14 00:00:00', 4, 1);
COMMIT;

-- ----------------------------
-- Table structure for evaluacioninterna
-- ----------------------------
DROP TABLE IF EXISTS `evaluacioninterna`;
CREATE TABLE `evaluacioninterna`  (
  `CodigoEI` int NOT NULL AUTO_INCREMENT,
  `CodigoBus` int NOT NULL,
  `Fecha` datetime NOT NULL,
  `TICodigo` int NOT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`CodigoEI`) USING BTREE,
  INDEX `CodigoBus`(`CodigoBus` ASC) USING BTREE,
  INDEX `TICodigo`(`TICodigo` ASC) USING BTREE,
  CONSTRAINT `evaluacioninterna_ibfk_1` FOREIGN KEY (`CodigoBus`) REFERENCES `bus` (`BusB`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `evaluacioninterna_ibfk_2` FOREIGN KEY (`TICodigo`) REFERENCES `ordentrabajointerno` (`CodigoTI`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of evaluacioninterna
-- ----------------------------
BEGIN;
INSERT INTO `evaluacioninterna` (`CodigoEI`, `CodigoBus`, `Fecha`, `TICodigo`, `Estado`) VALUES (1, 1, '2025-05-24 00:00:00', 1, 1), (2, 2, '2025-05-26 00:00:00', 2, 1), (3, 1, '2025-05-25 00:00:00', 3, 1), (4, 2, '2025-06-16 00:00:00', 4, 1), (5, 3, '2025-06-20 00:00:00', 5, 1), (6, 3, '2025-06-21 00:00:00', 6, 1);
COMMIT;

-- ----------------------------
-- Table structure for factura
-- ----------------------------
DROP TABLE IF EXISTS `factura`;
CREATE TABLE `factura`  (
  `CodigoFactura` int NOT NULL AUTO_INCREMENT,
  `CodigoOC` int NOT NULL,
  `Fecha` datetime NOT NULL,
  `TOTAL` decimal(18, 2) NOT NULL,
  PRIMARY KEY (`CodigoFactura`) USING BTREE,
  INDEX `CodigoOC`(`CodigoOC` ASC) USING BTREE,
  CONSTRAINT `factura_ibfk_1` FOREIGN KEY (`CodigoOC`) REFERENCES `ordencompra` (`CodigoOC`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 12 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of factura
-- ----------------------------
BEGIN;
INSERT INTO `factura` (`CodigoFactura`, `CodigoOC`, `Fecha`, `TOTAL`) VALUES (7, 1, '2025-05-29 00:00:00', 1500.75), (8, 2, '2025-05-28 00:00:00', 2750.00), (9, 3, '2025-05-27 00:00:00', 980.50), (10, 5, '2025-06-20 00:00:00', 340.00), (11, 6, '2025-06-21 00:00:00', 315.00);
COMMIT;

-- ----------------------------
-- Table structure for marcarepuesto
-- ----------------------------
DROP TABLE IF EXISTS `marcarepuesto`;
CREATE TABLE `marcarepuesto`  (
  `CodigoMR` int NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(80) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProveedorMR` int NOT NULL,
  `EstadoM` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`CodigoMR`) USING BTREE,
  INDEX `ProveedorMR`(`ProveedorMR` ASC) USING BTREE,
  CONSTRAINT `marcarepuesto_ibfk_1` FOREIGN KEY (`ProveedorMR`) REFERENCES `proveedor` (`CodigoP`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of marcarepuesto
-- ----------------------------
BEGIN;
INSERT INTO `marcarepuesto` (`CodigoMR`, `Descripcion`, `ProveedorMR`, `EstadoM`) VALUES (1, 'Bosch', 1, 1), (2, 'ACDelco', 2, 1), (3, 'Motorcraft', 3, 1), (4, 'MICOS', 2, 1), (5, 'COSCUS', 3, 1);
COMMIT;

-- ----------------------------
-- Table structure for mecanico
-- ----------------------------
DROP TABLE IF EXISTS `mecanico`;
CREATE TABLE `mecanico`  (
  `CodigoM` int NOT NULL AUTO_INCREMENT,
  `EspecialidadM` int NOT NULL,
  `Nombre` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DNI` varchar(8) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Domicilio` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Experiencia` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Telefono` varchar(9) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Sueldo` decimal(18, 2) NOT NULL,
  `Turno` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `FechaContrato` datetime NOT NULL,
  `EstadoM` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`CodigoM`) USING BTREE,
  INDEX `EspecialidadM`(`EspecialidadM` ASC) USING BTREE,
  CONSTRAINT `mecanico_ibfk_1` FOREIGN KEY (`EspecialidadM`) REFERENCES `especialidad` (`CodigoS`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `mecanico_chk_1` CHECK (regexp_like(`Telefono`,_utf8mb4'^[0-9]{9}$'))
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of mecanico
-- ----------------------------
BEGIN;
INSERT INTO `mecanico` (`CodigoM`, `EspecialidadM`, `Nombre`, `DNI`, `Domicilio`, `Experiencia`, `Telefono`, `Sueldo`, `Turno`, `FechaContrato`, `EstadoM`) VALUES (1, 3, 'Juan Pérez', '12345678', 'Av. Siempre Viva 123', '5 años', '999111222', 2500.00, 'Mañana', '2021-05-10 00:00:00', 1), (2, 2, 'Carlos Gómez', '87654321', 'Jr. Ejemplo 456', '3 años', '988222333', 2300.00, 'Tarde', '2022-03-15 00:00:00', 1), (3, 3, 'Luis Ramírez', '45678912', 'Calle Real 789', '7 años', '977333444', 2600.00, 'Mañana', '2020-01-20 00:00:00', 1), (4, 1, 'Jose', '75940375', 'Alfonso Ugarte', '2 años', '899843889', 1300.00, 'Mañana', '2020-03-20 00:00:00', 1), (5, 2, 'Alex', '85749304', 'Calle 5 de Junio', '8 años', '948822221', 6000.00, 'Noche', '2020-02-24 00:00:00', 1);
COMMIT;

-- ----------------------------
-- Table structure for notaingresorepuestos
-- ----------------------------
DROP TABLE IF EXISTS `notaingresorepuestos`;
CREATE TABLE `notaingresorepuestos`  (
  `CodigoIR` int NOT NULL AUTO_INCREMENT,
  `CodigoOC` int NOT NULL,
  `Fecha` datetime NOT NULL,
  `ProveedorIR` int NOT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`CodigoIR`) USING BTREE,
  INDEX `CodigoOC`(`CodigoOC` ASC) USING BTREE,
  INDEX `ProveedorIR`(`ProveedorIR` ASC) USING BTREE,
  CONSTRAINT `notaingresorepuestos_ibfk_2` FOREIGN KEY (`ProveedorIR`) REFERENCES `proveedor` (`CodigoP`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of notaingresorepuestos
-- ----------------------------
BEGIN;
INSERT INTO `notaingresorepuestos` (`CodigoIR`, `CodigoOC`, `Fecha`, `ProveedorIR`, `Estado`) VALUES (1, 1, '2025-05-28 10:00:00', 1, 1), (2, 2, '2025-05-27 14:30:00', 2, 1), (3, 3, '2025-05-26 09:15:00', 2, 1), (4, 4, '2025-06-16 00:00:00', 3, 1), (5, 5, '2025-06-20 00:00:00', 2, 1), (6, 6, '2025-06-21 00:00:00', 2, 1);
COMMIT;

-- ----------------------------
-- Table structure for notasalidarepuesto
-- ----------------------------
DROP TABLE IF EXISTS `notasalidarepuesto`;
CREATE TABLE `notasalidarepuesto`  (
  `CodigoSR` int NOT NULL AUTO_INCREMENT,
  `BusSR` int NOT NULL,
  `Fecha` datetime NOT NULL,
  `OPCodigo` int NOT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`CodigoSR`) USING BTREE,
  INDEX `BusSR`(`BusSR` ASC) USING BTREE,
  INDEX `OPCodigo`(`OPCodigo` ASC) USING BTREE,
  CONSTRAINT `notasalidarepuesto_ibfk_1` FOREIGN KEY (`BusSR`) REFERENCES `bus` (`BusB`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `notasalidarepuesto_ibfk_2` FOREIGN KEY (`OPCodigo`) REFERENCES `ordenpedido` (`CodigoOP`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 5 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of notasalidarepuesto
-- ----------------------------
BEGIN;
INSERT INTO `notasalidarepuesto` (`CodigoSR`, `BusSR`, `Fecha`, `OPCodigo`, `Estado`) VALUES (1, 1, '2025-05-28 08:00:00', 1, 1), (2, 2, '2025-05-27 09:30:00', 2, 1), (3, 2, '2025-05-26 11:15:00', 3, 1), (4, 2, '2025-06-16 00:00:00', 4, 1);
COMMIT;

-- ----------------------------
-- Table structure for ordencompra
-- ----------------------------
DROP TABLE IF EXISTS `ordencompra`;
CREATE TABLE `ordencompra`  (
  `CodigoOC` int NOT NULL AUTO_INCREMENT,
  `CodigoPro` int NOT NULL,
  `Fecha` datetime NOT NULL,
  `OPCodigo` int NOT NULL,
  `FormaPago` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `TOTAL` decimal(18, 2) NOT NULL,
  `EstadoC` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`CodigoOC`) USING BTREE,
  INDEX `CodigoPro`(`CodigoPro` ASC) USING BTREE,
  INDEX `OPCodigo`(`OPCodigo` ASC) USING BTREE,
  CONSTRAINT `ordencompra_ibfk_1` FOREIGN KEY (`CodigoPro`) REFERENCES `proveedor` (`CodigoP`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `ordencompra_ibfk_2` FOREIGN KEY (`OPCodigo`) REFERENCES `ordenpedido` (`CodigoOP`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of ordencompra
-- ----------------------------
BEGIN;
INSERT INTO `ordencompra` (`CodigoOC`, `CodigoPro`, `Fecha`, `OPCodigo`, `FormaPago`, `TOTAL`, `EstadoC`) VALUES (1, 1, '2025-05-24 00:00:00', 1, 'Efectivo', 1500.70, 1), (2, 2, '2025-05-26 00:00:00', 2, 'Transferencia', 2750.00, 1), (3, 1, '2025-05-25 00:00:00', 3, 'Efectivo', 980.50, 1), (4, 3, '2025-06-16 00:00:00', 4, 'Transferencia bancaria', 215.00, 1), (5, 2, '2025-06-20 00:00:00', 5, 'Transferencia bancaria', 340.00, 1), (6, 2, '2025-06-21 00:00:00', 6, 'Transferencia bancaria', 315.00, 1);
COMMIT;

-- ----------------------------
-- Table structure for ordenpedido
-- ----------------------------
DROP TABLE IF EXISTS `ordenpedido`;
CREATE TABLE `ordenpedido`  (
  `CodigoOP` int NOT NULL AUTO_INCREMENT,
  `Fecha` datetime NOT NULL,
  `TICodigo` int NOT NULL,
  `JefeEncargado` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Descripcion` varchar(80) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT 1,
  `Bus` int NULL DEFAULT NULL,
  PRIMARY KEY (`CodigoOP`) USING BTREE,
  INDEX `TICodigo`(`TICodigo` ASC) USING BTREE,
  INDEX `fk_ordenpedido_bus`(`Bus` ASC) USING BTREE,
  CONSTRAINT `fk_ordenpedido_bus` FOREIGN KEY (`Bus`) REFERENCES `bus` (`BusB`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `ordenpedido_ibfk_1` FOREIGN KEY (`TICodigo`) REFERENCES `ordentrabajointerno` (`CodigoTI`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of ordenpedido
-- ----------------------------
BEGIN;
INSERT INTO `ordenpedido` (`CodigoOP`, `Fecha`, `TICodigo`, `JefeEncargado`, `Descripcion`, `Estado`, `Bus`) VALUES (1, '2025-05-24 00:00:00', 1, 'Jefe de Taller', 'Pedido de repuestos para motor', 1, 1), (2, '2025-05-26 00:00:00', 2, 'Jefe de Taller', 'Compra de lubricantes y aceites', 1, 1), (3, '2025-05-25 00:00:00', 3, 'Jefe de Taller', 'Solicitud de herramientas especiales', 1, 1), (4, '2025-06-16 00:00:00', 4, 'Jefe de Almacen', 'Pedido de repuestos para la OTI', 1, 2), (5, '2025-06-20 00:00:00', 5, 'Jefe de Almacen', 'Repuestos solicitados para Mantenimiento', 1, 3), (6, '2025-06-21 00:00:00', 6, 'Jefe de Almacen', 'Repuestos para mantenimiento', 1, 3);
COMMIT;

-- ----------------------------
-- Table structure for ordentrabajoexterno
-- ----------------------------
DROP TABLE IF EXISTS `ordentrabajoexterno`;
CREATE TABLE `ordentrabajoexterno`  (
  `CodigoTE` int NOT NULL AUTO_INCREMENT,
  `CodigoBus` int NOT NULL,
  `ContratoCO` int NOT NULL,
  `Fecha` datetime NOT NULL,
  `ProveedorTE` int NOT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`CodigoTE`) USING BTREE,
  INDEX `CodigoBus`(`CodigoBus` ASC) USING BTREE,
  INDEX `ContratoCO`(`ContratoCO` ASC) USING BTREE,
  INDEX `ProveedorTE`(`ProveedorTE` ASC) USING BTREE,
  CONSTRAINT `ordentrabajoexterno_ibfk_1` FOREIGN KEY (`CodigoBus`) REFERENCES `bus` (`BusB`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `ordentrabajoexterno_ibfk_3` FOREIGN KEY (`ProveedorTE`) REFERENCES `proveedor` (`CodigoP`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 5 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of ordentrabajoexterno
-- ----------------------------
BEGIN;
INSERT INTO `ordentrabajoexterno` (`CodigoTE`, `CodigoBus`, `ContratoCO`, `Fecha`, `ProveedorTE`, `Estado`) VALUES (1, 1, 5, '2025-06-14 00:00:00', 1, 1), (2, 2, 6, '2025-06-14 00:00:00', 2, 1), (3, 1, 6, '2025-06-14 00:00:00', 2, 1), (4, 3, 7, '2025-06-14 00:00:00', 2, 1);
COMMIT;

-- ----------------------------
-- Table structure for ordentrabajointerno
-- ----------------------------
DROP TABLE IF EXISTS `ordentrabajointerno`;
CREATE TABLE `ordentrabajointerno`  (
  `CodigoTI` int NOT NULL AUTO_INCREMENT,
  `BusTI` int NOT NULL,
  `Fecha` datetime NOT NULL,
  `MecanicoTI` int NOT NULL,
  `Estado` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`CodigoTI`) USING BTREE,
  INDEX `BusTI`(`BusTI` ASC) USING BTREE,
  INDEX `MecanicoTI`(`MecanicoTI` ASC) USING BTREE,
  CONSTRAINT `ordentrabajointerno_ibfk_1` FOREIGN KEY (`BusTI`) REFERENCES `bus` (`BusB`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `ordentrabajointerno_ibfk_2` FOREIGN KEY (`MecanicoTI`) REFERENCES `mecanico` (`CodigoM`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 10 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of ordentrabajointerno
-- ----------------------------
BEGIN;
INSERT INTO `ordentrabajointerno` (`CodigoTI`, `BusTI`, `Fecha`, `MecanicoTI`, `Estado`) VALUES (1, 1, '2025-05-24 00:00:00', 1, 1), (2, 2, '2025-05-26 00:00:00', 2, 1), (3, 1, '2025-05-25 00:00:00', 3, 0), (4, 2, '2025-06-14 00:00:00', 1, 1), (5, 3, '2025-06-20 00:00:00', 2, 1), (6, 3, '2025-06-21 00:00:00', 1, 1), (7, 4, '2025-07-12 00:00:00', 1, 1), (8, 2, '2025-07-12 00:00:00', 4, 1), (9, 2, '2025-10-16 00:00:00', 2, 1);
COMMIT;

-- ----------------------------
-- Table structure for proveedor
-- ----------------------------
DROP TABLE IF EXISTS `proveedor`;
CREATE TABLE `proveedor`  (
  `CodigoP` int NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Direccion` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Telefono` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Correo` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `EstadoP` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`CodigoP`) USING BTREE,
  CONSTRAINT `proveedor_chk_1` CHECK (regexp_like(`Telefono`,_utf8mb4'^[0-9]{9}$'))
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of proveedor
-- ----------------------------
BEGIN;
INSERT INTO `proveedor` (`CodigoP`, `Nombre`, `Direccion`, `Telefono`, `Correo`, `EstadoP`) VALUES (1, 'Repuestos Perú S.A.', 'Av. Industrial 123, Lima', '987654321', 'contacto@repuestosperu.com', 1), (2, 'Sistemas Diesel SAC', 'Jr. Motor 456, Arequipa', '912345678', 'ventas@sistemasdiesel.com', 1), (3, 'AutoParts del Sur', 'Calle Repuestos 789, Cusco', '998877665', 'info@autopartssur.com', 1), (4, 'Linares', 'Av. Laureles', '995599666', 'linares@partes.com', 1), (5, 'Tecnología Peru', 'Jr. Huaylas 222', '998877665', 'ventasL.A.@gmail.com', 1);
COMMIT;

-- ----------------------------
-- Table structure for repuesto
-- ----------------------------
DROP TABLE IF EXISTS `repuesto`;
CREATE TABLE `repuesto`  (
  `CodigoR` int NOT NULL AUTO_INCREMENT,
  `NombreR` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CategoriaR` int NOT NULL,
  `MarcarepuestoR` int NOT NULL,
  `ProveedorR` int NOT NULL,
  `Precio` decimal(18, 2) NOT NULL,
  `EstadoR` tinyint(1) NOT NULL DEFAULT 1,
  PRIMARY KEY (`CodigoR`) USING BTREE,
  INDEX `CategoriaR`(`CategoriaR` ASC) USING BTREE,
  INDEX `MarcarepuestoR`(`MarcarepuestoR` ASC) USING BTREE,
  INDEX `ProveedorR`(`ProveedorR` ASC) USING BTREE,
  CONSTRAINT `repuesto_ibfk_1` FOREIGN KEY (`CategoriaR`) REFERENCES `categoria` (`CodigoC`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `repuesto_ibfk_2` FOREIGN KEY (`MarcarepuestoR`) REFERENCES `marcarepuesto` (`CodigoMR`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `repuesto_ibfk_3` FOREIGN KEY (`ProveedorR`) REFERENCES `proveedor` (`CodigoP`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 7 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of repuesto
-- ----------------------------
BEGIN;
INSERT INTO `repuesto` (`CodigoR`, `NombreR`, `CategoriaR`, `MarcarepuestoR`, `ProveedorR`, `Precio`, `EstadoR`) VALUES (1, 'Filtro de Aire', 1, 1, 1, 75.00, 1), (2, 'Pastilla de freno', 2, 2, 2, 22.50, 1), (3, 'Amortiguador delantero', 3, 3, 3, 240.00, 1), (4, 'Alicate', 4, 1, 1, 15.00, 1), (5, 'Gata', 4, 1, 1, 80.00, 1), (6, 'Compresor de aire', 7, 1, 1, 50.00, 1);
COMMIT;

-- ----------------------------
-- Table structure for usuario
-- ----------------------------
DROP TABLE IF EXISTS `usuario`;
CREATE TABLE `usuario`  (
  `Usu_Id` int NOT NULL AUTO_INCREMENT,
  `Usu_Nombre` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Usu_Password` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Usu_Rol` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`Usu_Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 8 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci;

-- ----------------------------
-- Records of usuario
-- ----------------------------
BEGIN;
INSERT INTO `usuario` (`Usu_Id`, `Usu_Nombre`, `Usu_Password`, `Usu_Rol`) VALUES (4, 'Jefe de Almacen', '123abc', 'Jefe de Almacen'), (5, 'Jefe de Mantenimiento', '456abc', 'Jefe de Mantenimiento'), (6, 'Jefe de Compras', '789abc', 'Jefe de Compras'), (7, 'Administrador', 'admin123', 'Administrador');
COMMIT;

-- ----------------------------
-- Procedure structure for SP_Bus_Actualiza
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Bus_Actualiza`;
delimiter ;;
CREATE PROCEDURE `SP_Bus_Actualiza`(IN p_BusB INT,
    IN p_Marca VARCHAR(30),
    IN p_Modelo VARCHAR(30),
    IN p_PisoBus VARCHAR(20),
    IN p_NPlaca VARCHAR(20),
    IN p_NChasis VARCHAR(20),
    IN p_NMotor VARCHAR(20),
    IN p_Capacidad INT,
    IN p_TipoMotor VARCHAR(20),
    IN p_Combustible VARCHAR(30),
    IN p_FechaAdquisicion DATETIME,
    IN p_Kilometraje INT,
    IN p_EstadoB TINYINT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_ExistePlaca INT;

    SELECT COUNT(*) INTO v_ExistePlaca
    FROM bus
    WHERE NPlaca = p_NPlaca AND BusB <> p_BusB;

    IF v_ExistePlaca > 0 THEN
        SET p_Mensaje = 'Ya existe otro bus con esa placa';
    ELSE
        UPDATE bus
        SET
            Marca = p_Marca,
            Modelo = p_Modelo,
            PisoBus = p_PisoBus,
            NPlaca = p_NPlaca,
            NChasis = p_NChasis,
            NMotor = p_NMotor,
            Capacidad = p_Capacidad,
            TipoMotor = p_TipoMotor,
            Combustible = p_Combustible,
            FechaAdquisicion = p_FechaAdquisicion,
            Kilometraje = p_Kilometraje,
            EstadoB = p_EstadoB
        WHERE BusB = p_BusB;

        SET p_Mensaje = 'Bus actualizado exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Bus_Crea
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Bus_Crea`;
delimiter ;;
CREATE PROCEDURE `SP_Bus_Crea`(IN p_Marca VARCHAR(30),
    IN p_Modelo VARCHAR(30),
    IN p_PisoBus VARCHAR(20),
    IN p_NPlaca VARCHAR(20),
    IN p_NChasis VARCHAR(20),
    IN p_NMotor VARCHAR(20),
    IN p_Capacidad INT,
    IN p_TipoMotor VARCHAR(20),
    IN p_Combustible VARCHAR(30),
    IN p_FechaAdquisicion DATETIME,
    IN p_Kilometraje INT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_ExistePlaca INT;

    SELECT COUNT(*) INTO v_ExistePlaca
    FROM bus
    WHERE NPlaca = p_NPlaca;

    IF v_ExistePlaca > 0 THEN
        SET p_Mensaje = 'Ya existe un bus con esa placa';
    ELSE
        INSERT INTO bus (
            Marca, Modelo, PisoBus, NPlaca, NChasis, NMotor,
            Capacidad, TipoMotor, Combustible, FechaAdquisicion,
            Kilometraje, EstadoB
        ) VALUES (
            p_Marca, p_Modelo, p_PisoBus, p_NPlaca, p_NChasis, p_NMotor,
            p_Capacidad, p_TipoMotor, p_Combustible, p_FechaAdquisicion,
            p_Kilometraje, 1
        );

        SET p_Mensaje = 'Bus creado exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Bus_Inactiva
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Bus_Inactiva`;
delimiter ;;
CREATE PROCEDURE `SP_Bus_Inactiva`(IN p_BusB INT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    -- Verifica si el bus existe y está activo
    IF EXISTS (SELECT 1 FROM bus WHERE BusB = p_BusB AND EstadoB = 1) THEN
        UPDATE bus
        SET EstadoB = 0
        WHERE BusB = p_BusB;

        SET p_Mensaje = 'Bus inactivado exitosamente';
    ELSE
        SET p_Mensaje = 'Bus no encontrado o ya está inactivo';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Bus_Lista
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Bus_Lista`;
delimiter ;;
CREATE PROCEDURE `SP_Bus_Lista`()
BEGIN
    SELECT 
        BusB, Marca, Modelo, PisoBus, NPlaca, NChasis, NMotor,
        Capacidad, TipoMotor, Combustible, FechaAdquisicion,
        Kilometraje, EstadoB
    FROM bus;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Bus_ObtenPorId
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Bus_ObtenPorId`;
delimiter ;;
CREATE PROCEDURE `SP_Bus_ObtenPorId`(IN p_BusB INT)
BEGIN
    SELECT 
        BusB, Marca, Modelo, PisoBus, NPlaca, NChasis, NMotor,
        Capacidad, TipoMotor, Combustible, FechaAdquisicion,
        Kilometraje, EstadoB
    FROM bus
    WHERE BusB = p_BusB;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Categoria_Actualiza
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Categoria_Actualiza`;
delimiter ;;
CREATE PROCEDURE `SP_Categoria_Actualiza`(IN p_CodigoC INT,
    IN p_NombreC VARCHAR(50),
    IN p_Descripcion VARCHAR(50),
    IN p_EstadoC TINYINT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    -- Realiza la actualización directamente sin verificar nombre duplicado
    UPDATE categoria
    SET NombreC = p_NombreC,
        Descripcion = p_Descripcion,
        EstadoC = p_EstadoC
    WHERE CodigoC = p_CodigoC;

    SET p_Mensaje = 'Categoría actualizada exitosamente';
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Categoria_Crea
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Categoria_Crea`;
delimiter ;;
CREATE PROCEDURE `SP_Categoria_Crea`(IN p_NombreC VARCHAR(50),
    IN p_Descripcion VARCHAR(50),
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_ExisteNombre INT;

    -- Verifica si ya existe una categoría con el mismo nombre
    SELECT COUNT(*) INTO v_ExisteNombre
    FROM categoria
    WHERE NombreC = p_NombreC;

    IF v_ExisteNombre > 0 THEN
        SET p_Mensaje = 'Ya existe una categoría con ese nombre';
    ELSE
        INSERT INTO categoria (NombreC, Descripcion, EstadoC)
        VALUES (p_NombreC, p_Descripcion, 1);

        SET p_Mensaje = 'Categoría creada exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Categoria_Inactiva
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Categoria_Inactiva`;
delimiter ;;
CREATE PROCEDURE `SP_Categoria_Inactiva`(IN p_CodigoC INT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    -- Verifica si la categoría existe y está activa
    IF EXISTS (SELECT 1 FROM categoria WHERE CodigoC = p_CodigoC) THEN
        UPDATE categoria
        SET EstadoC = 0
        WHERE CodigoC = p_CodigoC;

        SET p_Mensaje = 'Categoría inactivada exitosamente';
    ELSE
        SET p_Mensaje = 'Categoría no encontrada';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Categoria_Lista
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Categoria_Lista`;
delimiter ;;
CREATE PROCEDURE `SP_Categoria_Lista`()
BEGIN
    SELECT CodigoC, NombreC, Descripcion, EstadoC
    FROM categoria;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Categoria_ObtenPorId
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Categoria_ObtenPorId`;
delimiter ;;
CREATE PROCEDURE `SP_Categoria_ObtenPorId`(IN p_CodigoC INT)
BEGIN
    SELECT CodigoC, NombreC, Descripcion, EstadoC
    FROM categoria
    WHERE CodigoC = p_CodigoC;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_ContratoMantenimiento_Crea
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_ContratoMantenimiento_Crea`;
delimiter ;;
CREATE PROCEDURE `SP_ContratoMantenimiento_Crea`(IN p_BusPlaca VARCHAR(80),          -- Placa del bus
    IN p_Fecha DATETIME,
    IN p_ProveedorNombre VARCHAR(80),   -- Nombre del proveedor
    IN p_Descripcion VARCHAR(80),
    IN p_Costo DECIMAL(18,2),
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_BusId INT;
    DECLARE v_ProveedorId INT;

    -- Buscar ID del bus activo por placa
    SELECT BusB INTO v_BusId
    FROM Bus
    WHERE NPlaca = p_BusPlaca AND EstadoB = 1;

    -- Verificar si el proveedor existe y está activo
    SELECT CodigoP INTO v_ProveedorId
    FROM Proveedor
    WHERE Nombre = p_ProveedorNombre AND EstadoP = 1;

    -- Validaciones
    IF v_BusId IS NULL THEN
        SET p_Mensaje = 'El bus está inactivo o no existe';
    ELSEIF v_ProveedorId IS NULL THEN
        SET p_Mensaje = 'El proveedor está inactivo o no existe';
    ELSE
        -- Insertar el contrato con Estado = 1 (activo) por defecto
        INSERT INTO ContratoMantenimiento (
            BusCM, Fecha, ProveedorCM, Descripcion, Costo, Estado
        ) VALUES (
            v_BusId, p_Fecha, v_ProveedorId, p_Descripcion, p_Costo, 1
        );

        SET p_Mensaje = 'Contrato de mantenimiento creado exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_ContratoMantenimiento_Inactiva
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_ContratoMantenimiento_Inactiva`;
delimiter ;;
CREATE PROCEDURE `SP_ContratoMantenimiento_Inactiva`(IN p_CodigoCM INT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    -- Verifica si el contrato existe y está activo
    IF EXISTS (SELECT 1 FROM contratomantenimiento WHERE CodigoCM = p_CodigoCM AND Estado = 1) THEN
        UPDATE contratomantenimiento
        SET Estado = 0
        WHERE CodigoCM = p_CodigoCM;

        SET p_Mensaje = 'Contrato de mantenimiento inactivado exitosamente';
    ELSE
        SET p_Mensaje = 'Contrato no encontrado o ya está inactivo';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_ContratoMantenimiento_Lista
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_ContratoMantenimiento_Lista`;
delimiter ;;
CREATE PROCEDURE `SP_ContratoMantenimiento_Lista`()
BEGIN
    SELECT 
        cm.CodigoCM,
        cm.Fecha,
        cm.Descripcion,
        cm.Costo,
        cm.Estado,
        b.NPlaca AS Placa,
        p.Nombre AS Nombre
    FROM ContratoMantenimiento cm
    INNER JOIN Bus b ON cm.BusCM = b.BusB
    INNER JOIN Proveedor p ON cm.ProveedorCM = p.CodigoP;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_ContratoMantenimiento_ObtenPorId
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_ContratoMantenimiento_ObtenPorId`;
delimiter ;;
CREATE PROCEDURE `SP_ContratoMantenimiento_ObtenPorId`(IN p_CodigoCM INT)
BEGIN
    SELECT 
        cm.CodigoCM,
        cm.Fecha,
        cm.Descripcion,
        cm.Costo,
        cm.Estado,
        b.NPlaca,
        p.Nombre
    FROM ContratoMantenimiento cm
    INNER JOIN Bus b ON cm.BusCM = b.BusB
    INNER JOIN Proveedor p ON cm.ProveedorCM = p.CodigoP
    WHERE cm.CodigoCM = p_CodigoCM;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_DetalleEvaluacionInterna_Crea
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_DetalleEvaluacionInterna_Crea`;
delimiter ;;
CREATE PROCEDURE `SP_DetalleEvaluacionInterna_Crea`(IN p_EICodigo INT,
    IN p_MecanicoNombre VARCHAR(80),
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_EIExiste INT;
    DECLARE v_MecanicoID INT;
    DECLARE v_MecanicoActivo INT;

    -- Verificar si existe la evaluación interna
    SELECT COUNT(*) INTO v_EIExiste 
    FROM EvaluacionInterna 
    WHERE CodigoEI = p_EICodigo;

    -- Obtener ID y estado del mecánico por nombre
    SELECT CodigoM, EstadoM 
    INTO v_MecanicoID, v_MecanicoActivo
    FROM Mecanico 
    WHERE Nombre = p_MecanicoNombre 
    LIMIT 1;

    IF v_EIExiste = 0 THEN
        SET p_Mensaje = 'La evaluación interna no existe';
    ELSEIF v_MecanicoID IS NULL THEN
        SET p_Mensaje = 'El mecánico no existe';
    ELSEIF v_MecanicoActivo = 0 THEN
        SET p_Mensaje = 'El mecánico está inactivo';
    ELSE
        INSERT INTO DetalleEvaluacionInterna (EICodigo, MecanicoEI)
        VALUES (p_EICodigo, v_MecanicoID);
        SET p_Mensaje = 'Detalle de evaluación interna creado exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_DetalleEvaluacionInterna_Lista
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_DetalleEvaluacionInterna_Lista`;
delimiter ;;
CREATE PROCEDURE `SP_DetalleEvaluacionInterna_Lista`()
BEGIN
    SELECT 
        dei.DetalleEvaluacionInternaID,
        ei.CodigoEI,
        mec.Nombre,
        ei.Fecha
    FROM DetalleEvaluacionInterna dei
    INNER JOIN EvaluacionInterna ei ON dei.EICodigo = ei.CodigoEI
    INNER JOIN Mecanico mec ON dei.MecanicoEI = mec.CodigoM;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_DetalleEvaluacionInterna_ObtenPorId
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_DetalleEvaluacionInterna_ObtenPorId`;
delimiter ;;
CREATE PROCEDURE `SP_DetalleEvaluacionInterna_ObtenPorId`(IN p_DetalleID INT)
BEGIN
    SELECT 
        dei.DetalleEvaluacionInternaID,
        ei.CodigoEI,
        mec.Nombre,
        ei.Fecha
    FROM DetalleEvaluacionInterna dei
    INNER JOIN EvaluacionInterna ei ON dei.EICodigo = ei.CodigoEI
    INNER JOIN Mecanico mec ON dei.MecanicoEI = mec.CodigoM
    WHERE dei.DetalleEvaluacionInternaID = p_DetalleID;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_DetalleNotaIngreso_Crea
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_DetalleNotaIngreso_Crea`;
delimiter ;;
CREATE PROCEDURE `SP_DetalleNotaIngreso_Crea`(IN p_IRCodigo INT,
    IN p_CantidadRecibida INT,
    IN p_NombreRepu VARCHAR(100), -- ahora recibe el nombre
    IN p_CantidadAceptada INT,
    IN p_Precio DECIMAL(18,2),
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_NotaIngresoRepuestosExiste INT;
    DECLARE v_RepuestoExiste INT;
    DECLARE v_CodigoRepu INT;

    -- Verificar si existe la nota de ingreso
    SELECT COUNT(*) INTO v_NotaIngresoRepuestosExiste 
    FROM NotaIngresoRepuestos 
    WHERE CodigoIR = p_IRCodigo;

    -- Buscar el código del repuesto por nombre
    SELECT CodigoR INTO v_CodigoRepu 
    FROM Repuesto 
    WHERE NombreR = p_NombreRepu 
    LIMIT 1;

    -- Validar existencia del repuesto
    IF v_CodigoRepu IS NOT NULL THEN
        SET v_RepuestoExiste = 1;
    ELSE
        SET v_RepuestoExiste = 0;
    END IF;

    IF v_NotaIngresoRepuestosExiste = 0 THEN
        SET p_Mensaje = 'La nota de ingreso de repuestos no existe';
    ELSEIF v_RepuestoExiste = 0 THEN
        SET p_Mensaje = 'El repuesto no existe';
    ELSE
        INSERT INTO DetalleNotaIngreso (
            IRCodigo, CantidadRecibida, CodigoRepu, CantidadAceptada, Precio
        ) VALUES (
            p_IRCodigo, p_CantidadRecibida, v_CodigoRepu, p_CantidadAceptada, p_Precio
        );

        SET p_Mensaje = 'Detalle de nota de ingreso de repuestos creado exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_DetalleNotaIngreso_Lista
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_DetalleNotaIngreso_Lista`;
delimiter ;;
CREATE PROCEDURE `SP_DetalleNotaIngreso_Lista`()
BEGIN
    SELECT 
        dni.DetalleNotaIngresoID,
        nir.CodigoIR,
        dni.CantidadRecibida,
        dni.CantidadAceptada,
        dni.Precio,
        nir.Fecha AS FechaNotaIngreso,
        r.NombreR
    FROM DetalleNotaIngreso dni
    INNER JOIN NotaIngresoRepuestos nir ON dni.IRCodigo = nir.CodigoIR
    INNER JOIN Repuesto r ON dni.CodigoRepu = r.CodigoR;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_DetalleNotaIngreso_ObtenPorId
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_DetalleNotaIngreso_ObtenPorId`;
delimiter ;;
CREATE PROCEDURE `SP_DetalleNotaIngreso_ObtenPorId`(IN p_DetalleNotaIngresoID INT)
BEGIN
    SELECT 
        dni.DetalleNotaIngresoID,
        nir.CodigoIR,
        dni.CantidadRecibida,
        dni.CantidadAceptada,
        dni.Precio,
        nir.Fecha,
        r.NombreR
    FROM DetalleNotaIngreso dni
    INNER JOIN NotaIngresoRepuestos nir ON dni.IRCodigo = nir.CodigoIR
    INNER JOIN Repuesto r ON dni.CodigoRepu = r.CodigoR
    WHERE dni.DetalleNotaIngresoID = p_DetalleNotaIngresoID;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_DetalleNotaSalida_Crea
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_DetalleNotaSalida_Crea`;
delimiter ;;
CREATE PROCEDURE `SP_DetalleNotaSalida_Crea`(IN p_DSRCodigo INT,
    IN p_CantidadRecibida INT,
    IN p_NombreRepu VARCHAR(50), -- ahora se llama NombreRepu
    IN p_CantidadEnviada INT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_NotaSalidaExistente INT;
    DECLARE v_CodigoRepu INT;

    -- Verificar si la nota de salida existe
    SELECT COUNT(*) INTO v_NotaSalidaExistente 
    FROM NotaSalidaRepuesto 
    WHERE CodigoSR = p_DSRCodigo;

    -- Obtener el ID del repuesto desde su nombre
    SELECT CodigoR INTO v_CodigoRepu 
    FROM Repuesto 
    WHERE NombreR = p_NombreRepu 
    LIMIT 1;

    IF v_NotaSalidaExistente = 0 THEN
        SET p_Mensaje = 'La nota de salida no existe';
    ELSEIF v_CodigoRepu IS NULL THEN
        SET p_Mensaje = 'El repuesto no existe';
    ELSE
        INSERT INTO DetalleNotaSalida (
            DSRCodigo, CantidadRecibida, CodigoRepu, CantidadEnviada
        ) VALUES (
            p_DSRCodigo, p_CantidadRecibida, v_CodigoRepu, p_CantidadEnviada
        );

        SET p_Mensaje = 'Detalle de nota de salida creado exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_DetalleNotaSalida_Lista
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_DetalleNotaSalida_Lista`;
delimiter ;;
CREATE PROCEDURE `SP_DetalleNotaSalida_Lista`()
BEGIN
    SELECT 
        dns.DetalleNotaSalidaID,
        nsr.CodigoSR,
        dns.CantidadRecibida,
        dns.CantidadEnviada,
        nr.NombreR,
        nsr.Fecha AS FechaNotaSalida
    FROM DetalleNotaSalida dns
    INNER JOIN NotaSalidaRepuesto nsr ON dns.DSRCodigo = nsr.CodigoSR
    INNER JOIN Repuesto nr ON dns.CodigoRepu = nr.CodigoR;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_DetalleNotaSalida_ObtenPorId
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_DetalleNotaSalida_ObtenPorId`;
delimiter ;;
CREATE PROCEDURE `SP_DetalleNotaSalida_ObtenPorId`(IN p_DetalleNotaSalidaID INT)
BEGIN
    SELECT 
        dns.DetalleNotaSalidaID,
        nsr.CodigoSR,
        dns.CantidadRecibida,
        dns.CantidadEnviada,
        nr.NombreR,
        nsr.Fecha
    FROM DetalleNotaSalida dns
    INNER JOIN NotaSalidaRepuesto nsr ON dns.DSRCodigo = nsr.CodigoSR
    INNER JOIN Repuesto nr ON dns.CodigoRepu = nr.CodigoR
    WHERE dns.DetalleNotaSalidaID = p_DetalleNotaSalidaID;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_DetalleOrdenCompra_Crea
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_DetalleOrdenCompra_Crea`;
delimiter ;;
CREATE PROCEDURE `SP_DetalleOrdenCompra_Crea`(IN p_OCCompra INT,
    IN p_Cantidad INT,
    IN p_CodigoRep VARCHAR(50),
    IN p_Precio DECIMAL(18,2),
    IN p_CantidadAceptada INT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_OrdenCompraExistente INT;
    DECLARE v_IDRepuesto INT;

    -- Verificar si la orden de compra existe
    SELECT COUNT(*) INTO v_OrdenCompraExistente 
    FROM OrdenCompra 
    WHERE CodigoOC = p_OCCompra;

    -- Obtener el ID del repuesto por nombre
    SELECT CodigoR INTO v_IDRepuesto
    FROM Repuesto
    WHERE NombreR = p_CodigoRep;

    IF v_OrdenCompraExistente = 0 THEN
        SET p_Mensaje = 'La orden de compra no existe';
    ELSEIF v_IDRepuesto IS NULL THEN
        SET p_Mensaje = 'El repuesto no existe';
    ELSE
        -- Insertar el detalle con CantidadAceptada especificada
        INSERT INTO DetalleOrdenCompra (
            OCCompra, Cantidad, CodigoRep, Precio, CantidadAceptada
        ) VALUES (
            p_OCCompra, p_Cantidad, v_IDRepuesto, p_Precio, p_CantidadAceptada
        );

        SET p_Mensaje = 'Detalle de orden de compra creado exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_DetalleOrdenCompra_Lista
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_DetalleOrdenCompra_Lista`;
delimiter ;;
CREATE PROCEDURE `SP_DetalleOrdenCompra_Lista`()
BEGIN
    SELECT 
        doc.DetalleOrdenCompraID,
        oc.CodigoOC,
        doc.Cantidad,
        doc.CantidadAceptada,
        doc.Precio,
        rp.NombreR,
        oc.Fecha
    FROM DetalleOrdenCompra doc
    INNER JOIN OrdenCompra oc ON doc.OCCompra = oc.CodigoOC
    INNER JOIN Repuesto rp ON doc.CodigoRep = rp.CodigoR;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_DetalleOrdenCompra_ObtenPorId
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_DetalleOrdenCompra_ObtenPorId`;
delimiter ;;
CREATE PROCEDURE `SP_DetalleOrdenCompra_ObtenPorId`(IN p_DetalleOrdenCompraID INT)
BEGIN
    SELECT 
        doc.DetalleOrdenCompraID,
        oc.CodigoOC,
        doc.Cantidad,
        doc.CantidadAceptada,
        doc.Precio,
        rp.NombreR,
        oc.Fecha
    FROM DetalleOrdenCompra doc
    INNER JOIN OrdenCompra oc ON doc.OCCompra = oc.CodigoOC
    INNER JOIN Repuesto rp ON doc.CodigoRep = rp.CodigoR
    WHERE doc.DetalleOrdenCompraID = p_DetalleOrdenCompraID;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_DetalleOrdenPedido_Crea
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_DetalleOrdenPedido_Crea`;
delimiter ;;
CREATE PROCEDURE `SP_DetalleOrdenPedido_Crea`(IN p_OPCodigo INT,
    IN p_Cantidad INT,
    IN p_CodigoRepu VARCHAR(50),
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_OrdenPedidoExistente INT;
    DECLARE v_IDRepuesto INT;

    -- Verificar si la orden de pedido existe
    SELECT COUNT(*) INTO v_OrdenPedidoExistente FROM OrdenPedido WHERE CodigoOP = p_OPCodigo;

    -- Obtener el ID del repuesto por nombre
    SELECT CodigoR INTO v_IDRepuesto
    FROM Repuesto
    WHERE NombreR = p_CodigoRepu
    LIMIT 1;

    -- Si no se encontró el repuesto, v_IDRepuesto será NULL
    IF v_OrdenPedidoExistente = 0 THEN
        SET p_Mensaje = 'La orden de pedido no existe';
    ELSEIF v_IDRepuesto IS NULL THEN
        SET p_Mensaje = 'El repuesto no existe';
    ELSE
        -- Insertar el detalle con el ID del repuesto
        INSERT INTO DetalleOrdenPedido (
            OPCodigo, Cantidad, CodigoRepu
        ) VALUES (
            p_OPCodigo, p_Cantidad, v_IDRepuesto
        );

        SET p_Mensaje = 'Detalle de orden de pedido creado exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_DetalleOrdenPedido_Lista
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_DetalleOrdenPedido_Lista`;
delimiter ;;
CREATE PROCEDURE `SP_DetalleOrdenPedido_Lista`()
BEGIN
    SELECT 
        dop.DetalleOrdenPedidoID,
        op.CodigoOP,
        dop.Cantidad,
        rp.NombreR,
        op.Fecha 
    FROM DetalleOrdenPedido dop
    INNER JOIN OrdenPedido op ON dop.OPCodigo = op.CodigoOP
    INNER JOIN Repuesto rp ON dop.CodigoRepu = rp.CodigoR;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_DetalleOrdenPedido_ObtenPorId
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_DetalleOrdenPedido_ObtenPorId`;
delimiter ;;
CREATE PROCEDURE `SP_DetalleOrdenPedido_ObtenPorId`(IN p_DetalleOrdenPedidoID INT)
BEGIN
    SELECT 
        dop.DetalleOrdenPedidoID,
        op.CodigoOP,
        dop.Cantidad,
        dop.CodigoRepu,
        rp.NombreR,
        op.Fecha
    FROM DetalleOrdenPedido dop
    INNER JOIN OrdenPedido op ON dop.OPCodigo = op.CodigoOP
    INNER JOIN Repuesto rp ON dop.CodigoRepu = rp.CodigoR
    WHERE dop.DetalleOrdenPedidoID = p_DetalleOrdenPedidoID;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_DetalleOTE_Crea
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_DetalleOTE_Crea`;
delimiter ;;
CREATE PROCEDURE `SP_DetalleOTE_Crea`(IN p_TECodigo INT,
    IN p_CodigoRepu VARCHAR(30),
    IN p_Parte VARCHAR(30),
    IN p_Pieza VARCHAR(30),
    IN p_Cantidad INT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_OrdenTrabajoExternaExistente INT;
    DECLARE v_RepuestoExistente INT;
    DECLARE v_IdRepuesto INT;

    -- Verificar si la orden de trabajo externa existe
    SELECT COUNT(*) INTO v_OrdenTrabajoExternaExistente FROM OrdenTrabajoExterno WHERE CodigoTE = p_TECodigo;

    -- Buscar el ID del repuesto por su nombre
    SELECT CodigoR INTO v_IdRepuesto 
    FROM Repuesto 
    WHERE NombreR = p_CodigoRepu 
    LIMIT 1;

    -- Verificar si la orden de trabajo externa existe
    IF v_OrdenTrabajoExternaExistente = 0 THEN
        SET p_Mensaje = 'La orden de trabajo externa no existe';
    -- Verificar si el repuesto existe
    ELSEIF v_IdRepuesto IS NULL THEN
        SET p_Mensaje = 'El repuesto no existe';
    ELSE
        -- Insertar el detalle de la orden de trabajo externa con el ID del repuesto
        INSERT INTO DetalleOTE (
            TECodigo, CodigoRepu, Parte, Pieza, Cantidad
        ) VALUES (
            p_TECodigo, v_IdRepuesto, p_Parte, p_Pieza, p_Cantidad
        );

        SET p_Mensaje = 'Detalle de orden de trabajo externa creado exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_DetalleOTE_Lista
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_DetalleOTE_Lista`;
delimiter ;;
CREATE PROCEDURE `SP_DetalleOTE_Lista`()
BEGIN
    SELECT 
        doe.DetalleOTEID,
        ote.CodigoTE,
        ote.Fecha,
        r.NombreR,
        doe.Parte,
        doe.Pieza,
        doe.Cantidad
    FROM DetalleOTE doe
    INNER JOIN OrdenTrabajoExterno ote ON doe.TECodigo = ote.CodigoTE
    INNER JOIN Repuesto r ON doe.CodigoRepu = r.CodigoR;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_DetalleOTE_ObtenPorId
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_DetalleOTE_ObtenPorId`;
delimiter ;;
CREATE PROCEDURE `SP_DetalleOTE_ObtenPorId`(IN p_DetalleOTEID INT)
BEGIN
    SELECT 
        doe.DetalleOTEID,
        ote.CodigoTE,
        ote.Fecha,
        r.NombreR,
        doe.Parte,
        doe.Pieza,
        doe.Cantidad
    FROM DetalleOTE doe
    INNER JOIN OrdenTrabajoExterno ote ON doe.TECodigo = ote.CodigoTE
    INNER JOIN Repuesto r ON doe.CodigoRepu = r.CodigoR
    WHERE doe.DetalleOTEID = p_DetalleOTEID;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_DetalleOTI_Crea
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_DetalleOTI_Crea`;
delimiter ;;
CREATE PROCEDURE `SP_DetalleOTI_Crea`(IN p_OrdenTrabajoInternoID INT,
    IN p_CodigoRepu VARCHAR(40),      -- Aquí recibes el nombre del repuesto
    IN p_MecanicoTI VARCHAR(40),      -- Aquí recibes el nombre del mecánico
    IN p_Parte VARCHAR(40),
    IN p_Pieza VARCHAR(40),
    IN p_Cantidad INT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_OrdenExistente INT;
    DECLARE v_RepuestoID INT;
    DECLARE v_MecanicoID INT;

    -- Verificar si la orden de trabajo interna existe
    SELECT COUNT(*) INTO v_OrdenExistente 
    FROM OrdenTrabajoInterno 
    WHERE CodigoTI = p_OrdenTrabajoInternoID;

    -- Obtener el ID del repuesto
    SELECT CodigoR INTO v_RepuestoID 
    FROM Repuesto 
    WHERE NombreR = p_CodigoRepu 
    LIMIT 1;

    -- Obtener el ID del mecánico
    SELECT CodigoM INTO v_MecanicoID 
    FROM Mecanico 
    WHERE Nombre = p_MecanicoTI 
    LIMIT 1;

    -- Validaciones
    IF v_OrdenExistente = 0 THEN
        SET p_Mensaje = 'La orden de trabajo interna no existe';
    ELSEIF v_RepuestoID IS NULL THEN
        SET p_Mensaje = 'El repuesto no existe';
    ELSEIF v_MecanicoID IS NULL THEN
        SET p_Mensaje = 'El mecánico no existe';
    ELSE
        -- Insertar el detalle con los IDs correctos
        INSERT INTO DetalleOTI (
            OrdenTrabajoInternoID, CodigoRepu, MecanicoTI, Parte, Pieza, Cantidad
        ) VALUES (
            p_OrdenTrabajoInternoID, v_RepuestoID, v_MecanicoID, p_Parte, p_Pieza, p_Cantidad
        );

        SET p_Mensaje = 'Detalle de orden de trabajo interno creado exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_DetalleOTI_Lista
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_DetalleOTI_Lista`;
delimiter ;;
CREATE PROCEDURE `SP_DetalleOTI_Lista`()
BEGIN
    SELECT 
        doi.DetalleOTIID,
        oti.CodigoTI,
        oti.Fecha,
        r.NombreR,
        m.Nombre,
        doi.Parte,
        doi.Pieza,
        doi.Cantidad
    FROM DetalleOTI doi
    INNER JOIN OrdenTrabajoInterno oti ON doi.OrdenTrabajoInternoID = oti.CodigoTI
    INNER JOIN Repuesto r ON doi.CodigoRepu = r.CodigoR
    INNER JOIN Mecanico m ON doi.MecanicoTI = m.CodigoM;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_DetalleOTI_ObtenPorId
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_DetalleOTI_ObtenPorId`;
delimiter ;;
CREATE PROCEDURE `SP_DetalleOTI_ObtenPorId`(IN p_DetalleOTIID INT)
BEGIN
    SELECT 
        doi.DetalleOTIID,
        oti.CodigoTI,
        oti.Fecha,
        r.NombreR,
        m.Nombre,
        doi.Parte,
        doi.Pieza,
        doi.Cantidad
    FROM DetalleOTI doi
    INNER JOIN OrdenTrabajoInterno oti ON doi.OrdenTrabajoInternoID = oti.CodigoTI
    INNER JOIN Repuesto r ON doi.CodigoRepu = r.CodigoR
    INNER JOIN Mecanico m ON doi.MecanicoTI = m.CodigoM
    WHERE doi.DetalleOTIID = p_DetalleOTIID;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Especialidad_Actualiza
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Especialidad_Actualiza`;
delimiter ;;
CREATE PROCEDURE `SP_Especialidad_Actualiza`(IN p_CodigoS INT,
    IN p_NombreS VARCHAR(50),
    IN p_Descripcion VARCHAR(50),
    IN p_EstadoE TINYINT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_Existe INT;

    SELECT COUNT(*) INTO v_Existe
    FROM especialidad
    WHERE NombreS = p_NombreS AND CodigoS <> p_CodigoS;

    IF v_Existe > 0 THEN
        SET p_Mensaje = 'Ya existe otra especialidad con ese nombre';
    ELSE
        UPDATE especialidad
        SET NombreS = p_NombreS,
            Descripcion = p_Descripcion,
            EstadoE = p_EstadoE
        WHERE CodigoS = p_CodigoS;

        SET p_Mensaje = 'Especialidad actualizada exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Especialidad_Crea
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Especialidad_Crea`;
delimiter ;;
CREATE PROCEDURE `SP_Especialidad_Crea`(IN p_NombreS VARCHAR(50),
    IN p_Descripcion VARCHAR(50),
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_ExisteNombre INT;

    SELECT COUNT(*) INTO v_ExisteNombre
    FROM especialidad
    WHERE NombreS = p_NombreS;

    IF v_ExisteNombre > 0 THEN
        SET p_Mensaje = 'Ya existe una especialidad con ese nombre';
    ELSE
        INSERT INTO especialidad (NombreS, Descripcion, EstadoE)
        VALUES (p_NombreS, p_Descripcion, 1);

        SET p_Mensaje = 'Especialidad creada exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Especialidad_Inactiva
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Especialidad_Inactiva`;
delimiter ;;
CREATE PROCEDURE `SP_Especialidad_Inactiva`(IN p_CodigoS INT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_EstadoActual TINYINT;

    -- Intentar obtener el estado actual de la especialidad
    SELECT EstadoE INTO v_EstadoActual
    FROM especialidad
    WHERE CodigoS = p_CodigoS;

    -- Validar si se encontró la especialidad
    IF ROW_COUNT() = 0 THEN
        SET p_Mensaje = 'Especialidad no encontrada';
    ELSEIF v_EstadoActual = 0 THEN
        SET p_Mensaje = 'La especialidad ya está inactiva';
    ELSE
        UPDATE especialidad
        SET EstadoE = 0
        WHERE CodigoS = p_CodigoS;

        SET p_Mensaje = 'Especialidad inactivada exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Especialidad_Lista
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Especialidad_Lista`;
delimiter ;;
CREATE PROCEDURE `SP_Especialidad_Lista`()
BEGIN
    SELECT CodigoS, NombreS, Descripcion, EstadoE
    FROM especialidad;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Especialidad_ObtenPorId
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Especialidad_ObtenPorId`;
delimiter ;;
CREATE PROCEDURE `SP_Especialidad_ObtenPorId`(IN p_CodigoS INT)
BEGIN
    SELECT CodigoS, NombreS, Descripcion, EstadoE
    FROM especialidad
    WHERE CodigoS = p_CodigoS;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_EvaluacionExterna_Crea
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_EvaluacionExterna_Crea`;
delimiter ;;
CREATE PROCEDURE `SP_EvaluacionExterna_Crea`(IN p_CodigoBus VARCHAR(40),        -- Placa del bus
    IN p_ProveedorEE VARCHAR(40),      -- Nombre del proveedor
    IN p_Fecha DATETIME,
    IN p_TECodigo INT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_BusID INT;
    DECLARE v_ProveedorID INT;
    DECLARE v_OrdenTrabajoExternaExistente INT;

    -- Obtener el ID del bus
    SELECT BusB INTO v_BusID 
    FROM Bus 
    WHERE NPlaca = p_CodigoBus 
    LIMIT 1;

    -- Obtener el ID del proveedor
    SELECT CodigoP INTO v_ProveedorID 
    FROM Proveedor 
    WHERE Nombre = p_ProveedorEE 
    LIMIT 1;

    -- Verificar si la orden de trabajo externa existe
    SELECT COUNT(*) INTO v_OrdenTrabajoExternaExistente 
    FROM OrdenTrabajoExterno 
    WHERE CodigoTE = p_TECodigo;

    IF v_BusID IS NULL THEN
        SET p_Mensaje = 'El bus no existe';
    ELSEIF v_ProveedorID IS NULL THEN
        SET p_Mensaje = 'El proveedor no existe';
    ELSEIF v_OrdenTrabajoExternaExistente = 0 THEN
        SET p_Mensaje = 'La orden de trabajo externa no existe';
    ELSE
        INSERT INTO EvaluacionExterna (
            CodigoBus, ProveedorEE, Fecha, TECodigo, Estado
        ) VALUES (
            v_BusID, v_ProveedorID, p_Fecha, p_TECodigo, 1
        );

        SET p_Mensaje = 'Evaluación externa creada exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_EvaluacionExterna_Inactiva
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_EvaluacionExterna_Inactiva`;
delimiter ;;
CREATE PROCEDURE `SP_EvaluacionExterna_Inactiva`(IN p_CodigoEE INT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    -- Verifica si la evaluación externa existe y está activa
    IF EXISTS (SELECT 1 FROM EvaluacionExterna WHERE CodigoEE = p_CodigoEE AND Estado = 1) THEN
        UPDATE EvaluacionExterna
        SET Estado = 0
        WHERE CodigoEE = p_CodigoEE;

        SET p_Mensaje = 'Evaluación externa inactivada exitosamente';
    ELSE
        SET p_Mensaje = 'Evaluación externa no encontrada o ya está inactiva';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_EvaluacionExterna_Lista
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_EvaluacionExterna_Lista`;
delimiter ;;
CREATE PROCEDURE `SP_EvaluacionExterna_Lista`()
BEGIN
    SELECT 
        ee.CodigoEE,
        b.NPlaca,
        p.Nombre,
        ee.Fecha,
        ote.CodigoTE,
        ee.Estado
    FROM EvaluacionExterna ee
    INNER JOIN Bus b ON ee.CodigoBus = b.BusB
    INNER JOIN Proveedor p ON ee.ProveedorEE = p.CodigoP
    INNER JOIN OrdenTrabajoExterno ote ON ee.TECodigo = ote.CodigoTE;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_EvaluacionExterna_ObtenPorId
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_EvaluacionExterna_ObtenPorId`;
delimiter ;;
CREATE PROCEDURE `SP_EvaluacionExterna_ObtenPorId`(IN p_CodigoEE INT)
BEGIN
    SELECT 
        ee.CodigoEE,
        b.NPlaca,
        p.Nombre,
        ee.Fecha,
        ote.CodigoTE,
        ee.Estado
    FROM EvaluacionExterna ee
    INNER JOIN Bus b ON ee.CodigoBus = b.BusB
    INNER JOIN Proveedor p ON ee.ProveedorEE = p.CodigoP
    INNER JOIN OrdenTrabajoExterno ote ON ee.TECodigo = ote.CodigoTE
    WHERE ee.CodigoEE = p_CodigoEE;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_EvaluacionInterna_Crea
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_EvaluacionInterna_Crea`;
delimiter ;;
CREATE PROCEDURE `SP_EvaluacionInterna_Crea`(IN p_Placa VARCHAR(20),
    IN p_Fecha DATETIME,
    IN p_TICodigo INT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_BusID INT;
    DECLARE v_EstadoBus INT;
    DECLARE v_TIExiste INT;

    -- Buscar el ID del bus y su estado usando la placa
    SELECT BusB, EstadoB INTO v_BusID, v_EstadoBus 
    FROM Bus 
    WHERE NPlaca = p_Placa 
    LIMIT 1;

    -- Verificar si la orden de trabajo existe
    SELECT COUNT(*) INTO v_TIExiste 
    FROM OrdenTrabajoInterno 
    WHERE CodigoTI = p_TICodigo;

    IF v_BusID IS NULL THEN
        SET p_Mensaje = 'El bus no existe';
    ELSEIF v_EstadoBus = 0 THEN
        SET p_Mensaje = 'El bus está inactivo';
    ELSEIF v_TIExiste = 0 THEN
        SET p_Mensaje = 'La orden de trabajo interno no existe';
    ELSE
        INSERT INTO EvaluacionInterna (CodigoBus, Fecha, TICodigo, Estado)
        VALUES (v_BusID, p_Fecha, p_TICodigo, 1);

        SET p_Mensaje = 'Evaluación interna registrada exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_EvaluacionInterna_Inactiva
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_EvaluacionInterna_Inactiva`;
delimiter ;;
CREATE PROCEDURE `SP_EvaluacionInterna_Inactiva`(IN p_CodigoEI INT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    IF EXISTS (SELECT 1 FROM EvaluacionInterna WHERE CodigoEI = p_CodigoEI AND Estado = 1) THEN
        UPDATE EvaluacionInterna
        SET Estado = 0
        WHERE CodigoEI = p_CodigoEI;

        SET p_Mensaje = 'Evaluación interna inactivada exitosamente';
    ELSE
        SET p_Mensaje = 'Evaluación interna no encontrada o ya está inactiva';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_EvaluacionInterna_Lista
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_EvaluacionInterna_Lista`;
delimiter ;;
CREATE PROCEDURE `SP_EvaluacionInterna_Lista`()
BEGIN
    SELECT 
        ei.CodigoEI,
        b.NPlaca,
        ei.Fecha,
        ei.TICodigo,
        ei.Estado
    FROM EvaluacionInterna ei
    INNER JOIN Bus b ON ei.CodigoBus = b.BusB
    INNER JOIN OrdenTrabajoInterno oti ON ei.TICodigo = oti.CodigoTI;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_EvaluacionInterna_ObtenPorId
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_EvaluacionInterna_ObtenPorId`;
delimiter ;;
CREATE PROCEDURE `SP_EvaluacionInterna_ObtenPorId`(IN p_CodigoEI INT)
BEGIN
    SELECT 
        ei.CodigoEI,
        b.NPlaca,
        ei.TICodigo,
        ei.Fecha,
        ei.Estado
    FROM EvaluacionInterna ei
    INNER JOIN Bus b ON ei.CodigoBus = b.BusB
    INNER JOIN OrdenTrabajoInterno oti ON ei.TICodigo = oti.CodigoTI
    WHERE ei.CodigoEI = p_CodigoEI;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Factura_Crea
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Factura_Crea`;
delimiter ;;
CREATE PROCEDURE `SP_Factura_Crea`(IN p_CodigoOC INT,
    IN p_Fecha DATETIME,
    IN p_TOTAL DECIMAL(18,2),
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_OrdenCompraExistente INT;

    -- Verificar si la orden de compra existe
    SELECT COUNT(*) INTO v_OrdenCompraExistente FROM OrdenCompra WHERE CodigoOC = p_CodigoOC;

    IF v_OrdenCompraExistente = 0 THEN
        SET p_Mensaje = 'La orden de compra no existe';
    ELSE
        INSERT INTO Factura (
            CodigoOC, Fecha, TOTAL
        ) VALUES (
            p_CodigoOC, p_Fecha, p_TOTAL
        );

        SET p_Mensaje = 'Factura creada exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Factura_Lista
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Factura_Lista`;
delimiter ;;
CREATE PROCEDURE `SP_Factura_Lista`()
BEGIN
    SELECT 
        f.CodigoFactura,
        oc.CodigoOC,
        f.Fecha,
        f.TOTAL
    FROM Factura f
    INNER JOIN OrdenCompra oc ON f.CodigoOC = oc.CodigoOC;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Factura_ObtenPorId
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Factura_ObtenPorId`;
delimiter ;;
CREATE PROCEDURE `SP_Factura_ObtenPorId`(IN p_CodigoFactura INT)
BEGIN
    SELECT 
        f.CodigoFactura,
        oc.CodigoOC,
        f.Fecha,
        f.TOTAL
    FROM Factura f
    INNER JOIN OrdenCompra oc ON f.CodigoOC = oc.CodigoOC
    WHERE f.CodigoFactura = p_CodigoFactura;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Marcarepuesto_Actualiza
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Marcarepuesto_Actualiza`;
delimiter ;;
CREATE PROCEDURE `SP_Marcarepuesto_Actualiza`(IN p_CodigoMR INT,
    IN p_Descripcion VARCHAR(80),
    IN p_ProveedorMR VARCHAR(80), -- nombre del proveedor
    IN p_EstadoM TINYINT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_ExisteMarca INT;
    DECLARE v_ExisteProveedor INT;
    DECLARE v_IdProveedor INT;

    -- Verificar existencia de marca
    SELECT COUNT(*) INTO v_ExisteMarca
    FROM marcarepuesto
    WHERE CodigoMR = p_CodigoMR;

    -- Verificar existencia del proveedor activo
    SELECT COUNT(*) INTO v_ExisteProveedor
    FROM proveedor
    WHERE Nombre = p_ProveedorMR AND EstadoP = 1;

    -- Obtener ID del proveedor si existe y está activo
    IF v_ExisteProveedor > 0 THEN
        SELECT CodigoP INTO v_IdProveedor
        FROM proveedor
        WHERE Nombre = p_ProveedorMR AND EstadoP = 1
        LIMIT 1;
    END IF;

    -- Validaciones y actualización
    IF v_ExisteMarca = 0 THEN
        SET p_Mensaje = 'Marca de repuesto no encontrada';
    ELSEIF v_ExisteProveedor = 0 THEN
        SET p_Mensaje = 'Proveedor no válido o inactivo';
    ELSE
        UPDATE marcarepuesto
        SET Descripcion = p_Descripcion,
            ProveedorMR = v_IdProveedor,
            EstadoM = p_EstadoM
        WHERE CodigoMR = p_CodigoMR;

        SET p_Mensaje = 'Marca de repuesto actualizada exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Marcarepuesto_Crea
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Marcarepuesto_Crea`;
delimiter ;;
CREATE PROCEDURE `SP_Marcarepuesto_Crea`(IN p_Descripcion VARCHAR(80),
    IN p_ProveedorMR VARCHAR(80), -- ahora recibes el nombre del proveedor
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_IDProveedor INT;

    -- Buscar el ID del proveedor activo por su nombre
    SELECT CodigoP INTO v_IDProveedor
    FROM proveedor
    WHERE Nombre = p_ProveedorMR AND EstadoP = 1;

    -- Verificar si existe el proveedor
    IF v_IDProveedor IS NULL THEN
        SET p_Mensaje = 'El proveedor especificado no existe o está inactivo';
    ELSE
        -- Insertar con el ID del proveedor
        INSERT INTO marcarepuesto (Descripcion, ProveedorMR, EstadoM)
        VALUES (p_Descripcion, v_IDProveedor, 1);

        SET p_Mensaje = 'Marca de repuesto creada exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Marcarepuesto_Inactiva
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Marcarepuesto_Inactiva`;
delimiter ;;
CREATE PROCEDURE `SP_Marcarepuesto_Inactiva`(IN p_CodigoMR INT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_EstadoActual TINYINT;

    -- Intentar obtener el estado actual de la marca de repuesto
    SELECT EstadoM INTO v_EstadoActual
    FROM marcarepuesto
    WHERE CodigoMR = p_CodigoMR;

    -- Validar si la marca existe
    IF ROW_COUNT() = 0 THEN
        SET p_Mensaje = 'Marca de repuesto no encontrada';
    ELSEIF v_EstadoActual = 0 THEN
        SET p_Mensaje = 'La marca de repuesto ya está inactiva';
    ELSE
        UPDATE marcarepuesto
        SET EstadoM = 0
        WHERE CodigoMR = p_CodigoMR;

        SET p_Mensaje = 'Marca de repuesto inactivada exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Marcarepuesto_Lista
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Marcarepuesto_Lista`;
delimiter ;;
CREATE PROCEDURE `SP_Marcarepuesto_Lista`()
BEGIN
    SELECT 
        mr.CodigoMR,
        mr.Descripcion,
        mr.ProveedorMR,
        p.Nombre,
        mr.EstadoM
    FROM marcarepuesto mr
    INNER JOIN proveedor p ON mr.ProveedorMR = p.CodigoP;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Marcarepuesto_ObtenPorId
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Marcarepuesto_ObtenPorId`;
delimiter ;;
CREATE PROCEDURE `SP_Marcarepuesto_ObtenPorId`(IN p_CodigoMR INT)
BEGIN
    SELECT 
        mr.CodigoMR,
        mr.Descripcion,
        mr.ProveedorMR,
        p.Nombre AS Nombre,
        mr.EstadoM
    FROM marcarepuesto mr
    INNER JOIN proveedor p ON mr.ProveedorMR = p.CodigoP
    WHERE mr.CodigoMR = p_CodigoMR;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Mecanico_Actualiza
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Mecanico_Actualiza`;
delimiter ;;
CREATE PROCEDURE `SP_Mecanico_Actualiza`(IN p_CodigoM INT,
    IN p_EspecialidadM VARCHAR(50),
    IN p_Nombre VARCHAR(50),
    IN p_DNI VARCHAR(8),
    IN p_Domicilio VARCHAR(50),
    IN p_Experiencia VARCHAR(30),
    IN p_Telefono VARCHAR(9),
    IN p_Sueldo DECIMAL(18,2),
    IN p_Turno VARCHAR(20),
    IN p_FechaContrato DATETIME,
    IN p_EstadoM TINYINT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_ExisteDNI INT;
    DECLARE v_ExisteEspecialidad INT;
    DECLARE v_IdEspecialidad INT;

    -- Verificar si ya existe otro mecánico con el mismo DNI
    SELECT COUNT(*) INTO v_ExisteDNI 
    FROM mecanico 
    WHERE DNI = p_DNI AND CodigoM <> p_CodigoM;

    -- Verificar si la especialidad existe y está activa
    SELECT COUNT(*) INTO v_ExisteEspecialidad 
    FROM especialidad 
    WHERE NombreS = p_EspecialidadM AND EstadoE = 1;

    -- Obtener el ID de la especialidad (si existe)
    SELECT CodigoS INTO v_IdEspecialidad
    FROM especialidad
    WHERE NombreS = p_EspecialidadM AND EstadoE = 1
    LIMIT 1;

    -- Validaciones
    IF v_ExisteDNI > 0 THEN
        SET p_Mensaje = 'Ya existe otro mecánico con ese DNI';
    ELSEIF v_ExisteEspecialidad = 0 THEN
        SET p_Mensaje = 'La especialidad no existe o está inactiva';
    ELSE
        -- Actualizar el registro del mecánico
        UPDATE mecanico
        SET EspecialidadM = v_IdEspecialidad,
            Nombre = p_Nombre,
            DNI = p_DNI,
            Domicilio = p_Domicilio,
            Experiencia = p_Experiencia,
            Telefono = p_Telefono,
            Sueldo = p_Sueldo,
            Turno = p_Turno,
            FechaContrato = p_FechaContrato,
            EstadoM = p_EstadoM
        WHERE CodigoM = p_CodigoM;

        SET p_Mensaje = 'Mecánico actualizado exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Mecanico_Crea
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Mecanico_Crea`;
delimiter ;;
CREATE PROCEDURE `SP_Mecanico_Crea`(IN p_EspecialidadM VARCHAR(50),  -- ahora recibe el nombre de la especialidad
    IN p_Nombre VARCHAR(50),
    IN p_DNI VARCHAR(8),
    IN p_Domicilio VARCHAR(50),
    IN p_Experiencia VARCHAR(30),
    IN p_Telefono VARCHAR(9),
    IN p_Sueldo DECIMAL(18,2),
    IN p_Turno VARCHAR(20),
    IN p_FechaContrato DATETIME,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_ExisteDNI INT;
    DECLARE v_CodigoEspecialidad INT;

    SELECT COUNT(*) INTO v_ExisteDNI FROM mecanico WHERE DNI = p_DNI;
    
    SELECT CodigoS INTO v_CodigoEspecialidad 
    FROM especialidad 
    WHERE NombreS = p_EspecialidadM AND EstadoE = 1;

    IF v_ExisteDNI > 0 THEN
        SET p_Mensaje = 'Ya existe un mecánico con ese DNI';
    ELSEIF v_CodigoEspecialidad IS NULL THEN
        SET p_Mensaje = 'La especialidad no existe o está inactiva';
    ELSE
        INSERT INTO mecanico (
            EspecialidadM, Nombre, DNI, Domicilio, Experiencia,
            Telefono, Sueldo, Turno, FechaContrato, EstadoM
        ) VALUES (
            v_CodigoEspecialidad, p_Nombre, p_DNI, p_Domicilio, p_Experiencia,
            p_Telefono, p_Sueldo, p_Turno, p_FechaContrato, 1
        );

        SET p_Mensaje = 'Mecánico creado exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Mecanico_Inactiva
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Mecanico_Inactiva`;
delimiter ;;
CREATE PROCEDURE `SP_Mecanico_Inactiva`(IN p_CodigoM INT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_EstadoActual TINYINT;

    -- Obtener el estado actual del mecánico
    SELECT EstadoM INTO v_EstadoActual
    FROM mecanico
    WHERE CodigoM = p_CodigoM;

    -- Validar si el mecánico existe
    IF ROW_COUNT() = 0 THEN
        SET p_Mensaje = 'Mecánico no encontrado';
    ELSEIF v_EstadoActual = 0 THEN
        SET p_Mensaje = 'El mecánico ya está inactivo';
    ELSE
        UPDATE mecanico SET EstadoM = 0 WHERE CodigoM = p_CodigoM;
        SET p_Mensaje = 'Mecánico inactivado exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Mecanico_Lista
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Mecanico_Lista`;
delimiter ;;
CREATE PROCEDURE `SP_Mecanico_Lista`()
BEGIN
    SELECT 
        m.CodigoM,
        e.NombreS,
        m.Nombre,
        m.DNI,
        m.Domicilio,
        m.Experiencia,
        m.Telefono,
        m.Sueldo,
        m.Turno,
        m.FechaContrato,
        m.EstadoM
    FROM mecanico m
    INNER JOIN especialidad e ON m.EspecialidadM = e.CodigoS;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Mecanico_ObtenPorId
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Mecanico_ObtenPorId`;
delimiter ;;
CREATE PROCEDURE `SP_Mecanico_ObtenPorId`(IN p_CodigoM INT)
BEGIN
    SELECT 
        m.CodigoM,
        m.EspecialidadM,
        e.NombreS,
        m.Nombre,
        m.DNI,
        m.Domicilio,
        m.Experiencia,
        m.Telefono,
        m.Sueldo,
        m.Turno,
        m.FechaContrato,
        m.EstadoM
    FROM mecanico m
    INNER JOIN especialidad e ON m.EspecialidadM = e.CodigoS
    WHERE m.CodigoM = p_CodigoM;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_NotaIngresoRepuestos_Crea
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_NotaIngresoRepuestos_Crea`;
delimiter ;;
CREATE PROCEDURE `SP_NotaIngresoRepuestos_Crea`(IN p_CodigoOC INT,
    IN p_Fecha DATETIME,
    IN p_ProveedorIR VARCHAR(30),
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_OrdenCompraExiste INT;
    DECLARE v_ProveedorID INT;

    -- Validar si la orden de compra existe
    SELECT COUNT(*) INTO v_OrdenCompraExiste 
    FROM OrdenCompra 
    WHERE CodigoOC = p_CodigoOC;
    
    -- Obtener el ID del proveedor por nombre
    SELECT CodigoP INTO v_ProveedorID 
    FROM Proveedor 
    WHERE Nombre = p_ProveedorIR 
    LIMIT 1;

    IF v_OrdenCompraExiste = 0 THEN
        SET p_Mensaje = 'La orden de compra no existe';
    ELSEIF v_ProveedorID IS NULL THEN
        SET p_Mensaje = 'El proveedor no existe';
    ELSE
        INSERT INTO NotaIngresoRepuestos (
            CodigoOC, Fecha, ProveedorIR, Estado
        ) VALUES (
            p_CodigoOC, p_Fecha, v_ProveedorID, 1
        );

        SET p_Mensaje = 'Nota de ingreso de repuestos creada exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_NotaIngresoRepuestos_Inactiva
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_NotaIngresoRepuestos_Inactiva`;
delimiter ;;
CREATE PROCEDURE `SP_NotaIngresoRepuestos_Inactiva`(IN p_CodigoIR INT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    IF EXISTS (SELECT 1 FROM NotaIngresoRepuestos WHERE CodigoIR = p_CodigoIR AND Estado = 1) THEN
        UPDATE NotaIngresoRepuestos
        SET Estado = 0
        WHERE CodigoIR = p_CodigoIR;

        SET p_Mensaje = 'Nota de ingreso inactivada exitosamente';
    ELSE
        SET p_Mensaje = 'Nota de ingreso no encontrada o ya está inactiva';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_NotaIngresoRepuestos_Lista
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_NotaIngresoRepuestos_Lista`;
delimiter ;;
CREATE PROCEDURE `SP_NotaIngresoRepuestos_Lista`()
BEGIN
    SELECT 
        nir.CodigoIR,
        oc.CodigoOC,
        nir.Fecha,
        nir.Estado,
        oc.Fecha,
        p.Nombre 
    FROM NotaIngresoRepuestos nir
    INNER JOIN OrdenCompra oc ON nir.CodigoOC = oc.CodigoOC
    INNER JOIN Proveedor p ON nir.ProveedorIR = p.CodigoP;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_NotaIngresoRepuestos_ObtenPorId
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_NotaIngresoRepuestos_ObtenPorId`;
delimiter ;;
CREATE PROCEDURE `SP_NotaIngresoRepuestos_ObtenPorId`(IN p_CodigoIR INT)
BEGIN
    SELECT 
        nir.CodigoIR,
        oc.CodigoOC,
        nir.Fecha,
        nir.Estado,
        oc.Fecha,
        p.Nombre
    FROM NotaIngresoRepuestos nir
    INNER JOIN OrdenCompra oc ON nir.CodigoOC = oc.CodigoOC
    INNER JOIN Proveedor p ON nir.ProveedorIR = p.CodigoP
    WHERE nir.CodigoIR = p_CodigoIR;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_NotaSalidaRepuesto_Crea
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_NotaSalidaRepuesto_Crea`;
delimiter ;;
CREATE PROCEDURE `SP_NotaSalidaRepuesto_Crea`(IN p_BusSR VARCHAR(30),
    IN p_Fecha DATETIME,
    IN p_OPCodigo INT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_BusID INT;
    DECLARE v_BusActivo INT;
    DECLARE v_OrdenPedidoActivo INT;

    -- Buscar ID y estado del bus usando la placa
    SELECT BusB, EstadoB INTO v_BusID, v_BusActivo 
    FROM Bus 
    WHERE NPlaca = p_BusSR 
    LIMIT 1;

    -- Verificar si la orden de pedido está activa
    SELECT COUNT(*) INTO v_OrdenPedidoActivo 
    FROM OrdenPedido 
    WHERE CodigoOP = p_OPCodigo;

    IF v_BusID IS NULL THEN
        SET p_Mensaje = 'El bus no existe';
    ELSEIF v_BusActivo = 0 THEN
        SET p_Mensaje = 'El bus está inactivo';
    ELSEIF v_OrdenPedidoActivo = 0 THEN
        SET p_Mensaje = 'La orden de pedido no existe o está inactiva';
    ELSE
        INSERT INTO NotaSalidaRepuesto (
            BusSR, Fecha, OPCodigo, Estado
        ) VALUES (
            v_BusID, p_Fecha, p_OPCodigo, 1
        );

        SET p_Mensaje = 'Nota de salida de repuestos creada exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_NotaSalidaRepuesto_Inactiva
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_NotaSalidaRepuesto_Inactiva`;
delimiter ;;
CREATE PROCEDURE `SP_NotaSalidaRepuesto_Inactiva`(IN p_CodigoSR INT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    IF EXISTS (SELECT 1 FROM NotaSalidaRepuesto WHERE CodigoSR = p_CodigoSR AND Estado = 1) THEN
        UPDATE NotaSalidaRepuesto
        SET Estado = 0
        WHERE CodigoSR = p_CodigoSR;

        SET p_Mensaje = 'Nota de salida inactivada exitosamente';
    ELSE
        SET p_Mensaje = 'Nota de salida no encontrada o ya está inactiva';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_NotaSalidaRepuesto_Lista
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_NotaSalidaRepuesto_Lista`;
delimiter ;;
CREATE PROCEDURE `SP_NotaSalidaRepuesto_Lista`()
BEGIN
    SELECT 
        nsr.CodigoSR,
        op.CodigoOP,
        nsr.Fecha,
        nsr.Estado,
        bus.NPlaca
    FROM NotaSalidaRepuesto nsr
    INNER JOIN Bus bus ON nsr.BusSR = bus.BusB
    INNER JOIN OrdenPedido op ON nsr.OPCodigo = op.CodigoOP;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_NotaSalidaRepuesto_ObtenPorId
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_NotaSalidaRepuesto_ObtenPorId`;
delimiter ;;
CREATE PROCEDURE `SP_NotaSalidaRepuesto_ObtenPorId`(IN p_CodigoSR INT)
BEGIN
    SELECT 
        nsr.CodigoSR,
        nsr.Fecha,
        nsr.Estado,
        bus.NPlaca,
        op.CodigoOP
    FROM NotaSalidaRepuesto nsr
    INNER JOIN Bus bus ON nsr.BusSR = bus.BusB
    INNER JOIN OrdenPedido op ON nsr.OPCodigo = op.CodigoOP
    WHERE nsr.CodigoSR = p_CodigoSR;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_OrdenCompra_Crea
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_OrdenCompra_Crea`;
delimiter ;;
CREATE PROCEDURE `SP_OrdenCompra_Crea`(IN p_NombrePro VARCHAR(30),
    IN p_Fecha DATETIME,
    IN p_OPCodigo INT,
    IN p_FormaPago VARCHAR(50),
    IN p_TOTAL DECIMAL(18,2),
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_ProveedorID INT;
    DECLARE v_ProveedorExiste INT;
    DECLARE v_OrdenPedidoExiste INT;

    -- Buscar ID del proveedor usando el nombre
    SELECT CodigoP INTO v_ProveedorID 
    FROM Proveedor 
    WHERE Nombre = p_NombrePro 
    LIMIT 1;

    -- Validar existencia del proveedor
    SELECT COUNT(*) INTO v_ProveedorExiste 
    FROM Proveedor 
    WHERE Nombre = p_NombrePro;

    -- Validar existencia de la orden de pedido
    SELECT COUNT(*) INTO v_OrdenPedidoExiste 
    FROM OrdenPedido 
    WHERE CodigoOP = p_OPCodigo;

    IF v_ProveedorID IS NULL THEN
        SET p_Mensaje = 'El proveedor no existe';
    ELSEIF v_OrdenPedidoExiste = 0 THEN
        SET p_Mensaje = 'La orden de pedido no existe';
    ELSE
        -- Insertar la orden de compra con el ID del proveedor y Estado = 1
        INSERT INTO OrdenCompra (
            CodigoPro, Fecha, OPCodigo, FormaPago, TOTAL, EstadoC
        ) VALUES (
            v_ProveedorID, p_Fecha, p_OPCodigo, p_FormaPago, p_TOTAL, 1
        );

        SET p_Mensaje = 'Orden de compra creada exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_OrdenCompra_Inactiva
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_OrdenCompra_Inactiva`;
delimiter ;;
CREATE PROCEDURE `SP_OrdenCompra_Inactiva`(IN p_CodigoOC INT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    -- Verifica si la orden de compra existe y está activa
    IF EXISTS (SELECT 1 FROM OrdenCompra WHERE CodigoOC = p_CodigoOC AND EstadoC = 1) THEN
        UPDATE OrdenCompra
        SET EstadoC = 0
        WHERE CodigoOC = p_CodigoOC;

        SET p_Mensaje = 'Orden de compra inactiva exitosamente';
    ELSE
        SET p_Mensaje = 'Orden de compra no encontrada o ya está inactiva';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_OrdenCompra_Lista
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_OrdenCompra_Lista`;
delimiter ;;
CREATE PROCEDURE `SP_OrdenCompra_Lista`()
BEGIN
    SELECT 
        oc.CodigoOC,
        oc.Fecha,
        oc.FormaPago,
        oc.TOTAL,
        oc.EstadoC,
        p.Nombre,
        op.CodigoOP
    FROM OrdenCompra oc
    INNER JOIN Proveedor p ON oc.CodigoPro = p.CodigoP
    INNER JOIN OrdenPedido op ON oc.OPCodigo = op.CodigoOP;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_OrdenCompra_ObtenPorId
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_OrdenCompra_ObtenPorId`;
delimiter ;;
CREATE PROCEDURE `SP_OrdenCompra_ObtenPorId`(IN p_CodigoOC INT)
BEGIN
    SELECT 
        oc.CodigoOC,
        oc.Fecha,
        oc.FormaPago,
        oc.TOTAL,
        oc.EstadoC,
        p.Nombre,
        op.CodigoOP
    FROM OrdenCompra oc
    INNER JOIN Proveedor p ON oc.CodigoPro = p.CodigoP
    INNER JOIN OrdenPedido op ON oc.OPCodigo = op.CodigoOP
    WHERE oc.CodigoOC = p_CodigoOC;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_OrdenPedido_Crea
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_OrdenPedido_Crea`;
delimiter ;;
CREATE PROCEDURE `SP_OrdenPedido_Crea`(IN p_BusPlaca VARCHAR(30),
    IN p_Fecha DATETIME,
    IN p_TICodigo INT,
    IN p_JefeEncargado VARCHAR(50),
    IN p_Descripcion VARCHAR(80),
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_TIExiste INT;
    DECLARE v_BusID INT;
    DECLARE v_BusActivo INT;

    -- Verifica existencia de la orden interna
    SELECT COUNT(*) INTO v_TIExiste 
    FROM OrdenTrabajoInterno 
    WHERE CodigoTI = p_TICodigo;

    -- Busca el bus por su placa
    SELECT BusB, EstadoB INTO v_BusID, v_BusActivo 
    FROM Bus 
    WHERE NPlaca = p_BusPlaca 
    LIMIT 1;

    -- Validaciones
    IF v_TIExiste = 0 THEN
        SET p_Mensaje = 'La orden de trabajo interna no existe';
    ELSEIF v_BusID IS NULL THEN
        SET p_Mensaje = 'El bus no existe';
    ELSEIF v_BusActivo = 0 THEN
        SET p_Mensaje = 'El bus está inactivo';
    ELSE
        INSERT INTO OrdenPedido (
            Fecha, TICodigo, JefeEncargado, Descripcion, Estado, Bus
        ) VALUES (
            p_Fecha, p_TICodigo, p_JefeEncargado, p_Descripcion, 1, v_BusID
        );

        SET p_Mensaje = 'Orden de pedido creada exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_OrdenPedido_Inactiva
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_OrdenPedido_Inactiva`;
delimiter ;;
CREATE PROCEDURE `SP_OrdenPedido_Inactiva`(IN p_CodigoOP INT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    IF EXISTS (SELECT 1 FROM OrdenPedido WHERE CodigoOP = p_CodigoOP AND Estado = 1) THEN
        UPDATE OrdenPedido
        SET Estado = 0
        WHERE CodigoOP = p_CodigoOP;

        SET p_Mensaje = 'Orden de pedido inactivada exitosamente';
    ELSE
        SET p_Mensaje = 'Orden de pedido no encontrada o ya está inactiva';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_OrdenPedido_Lista
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_OrdenPedido_Lista`;
delimiter ;;
CREATE PROCEDURE `SP_OrdenPedido_Lista`()
BEGIN
    SELECT 
        op.CodigoOP,
        op.Fecha,
        op.JefeEncargado,
        op.Descripcion,
        op.Estado,
        ti.CodigoTI,
        b.NPlaca  -- Mostrar placa del bus
    FROM OrdenPedido op
    INNER JOIN OrdenTrabajoInterno ti ON op.TICodigo = ti.CodigoTI
    LEFT JOIN Bus b ON op.Bus = b.BusB;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_OrdenPedido_ObtenPorId
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_OrdenPedido_ObtenPorId`;
delimiter ;;
CREATE PROCEDURE `SP_OrdenPedido_ObtenPorId`(IN p_CodigoOP INT)
BEGIN
    SELECT 
        op.CodigoOP,
        op.Fecha,
        op.JefeEncargado,
        op.Descripcion,
        op.Estado,
        ti.CodigoTI,
        b.NPlaca  -- Mostrar placa del bus
    FROM OrdenPedido op
    INNER JOIN OrdenTrabajoInterno ti ON op.TICodigo = ti.CodigoTI
    LEFT JOIN Bus b ON op.Bus = b.BusB
    WHERE op.CodigoOP = p_CodigoOP;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_OrdenTI_Crea
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_OrdenTI_Crea`;
delimiter ;;
CREATE PROCEDURE `SP_OrdenTI_Crea`(IN p_BusTI VARCHAR(30),
    IN p_Fecha DATETIME,
    IN p_MecanicoTI VARCHAR(30),
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_BusID INT;
    DECLARE v_BusActivo INT;
    DECLARE v_MecanicoID INT;
    DECLARE v_MecanicoActivo INT;

    -- Obtener ID y estado del bus por placa
    SELECT BusB, EstadoB INTO v_BusID, v_BusActivo 
    FROM Bus 
    WHERE NPlaca = p_BusTI 
    LIMIT 1;

    -- Obtener ID y estado del mecánico por nombre
    SELECT CodigoM, EstadoM INTO v_MecanicoID, v_MecanicoActivo 
    FROM Mecanico 
    WHERE Nombre = p_MecanicoTI 
    LIMIT 1;

    -- Validaciones
    IF v_BusID IS NULL THEN
        SET p_Mensaje = 'El bus no existe';
    ELSEIF v_BusActivo = 0 THEN
        SET p_Mensaje = 'El bus está inactivo';
    ELSEIF v_MecanicoID IS NULL THEN
        SET p_Mensaje = 'El mecánico no existe';
    ELSEIF v_MecanicoActivo = 0 THEN
        SET p_Mensaje = 'El mecánico está inactivo';
    ELSE
        INSERT INTO OrdenTrabajoInterno (
            BusTI, Fecha, MecanicoTI, Estado
        ) VALUES (
            v_BusID, p_Fecha, v_MecanicoID, 1
        );

        SET p_Mensaje = 'Orden de trabajo creada exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_OrdenTI_Lista
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_OrdenTI_Lista`;
delimiter ;;
CREATE PROCEDURE `SP_OrdenTI_Lista`()
BEGIN
    SELECT 
        o.CodigoTI,
        b.NPlaca,
        m.CodigoM,
        m.Nombre,
        o.Fecha,
        o.Estado
    FROM OrdenTrabajoInterno o
    INNER JOIN Bus b ON o.BusTI = b.BusB
    INNER JOIN Mecanico m ON o.MecanicoTI = m.CodigoM;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_OrdenTI_ObtenPorId
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_OrdenTI_ObtenPorId`;
delimiter ;;
CREATE PROCEDURE `SP_OrdenTI_ObtenPorId`(IN p_CodigoTI INT)
BEGIN
    SELECT 
        o.CodigoTI,
        b.NPlaca,
        m.Nombre,
        o.Fecha,
        o.Estado
    FROM OrdenTrabajoInterno o
    INNER JOIN Bus b ON o.BusTI = b.BusB
    INNER JOIN Mecanico m ON o.MecanicoTI = m.CodigoM
    WHERE o.CodigoTI = p_CodigoTI;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_OrdenTrabajoExterno_Crea
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_OrdenTrabajoExterno_Crea`;
delimiter ;;
CREATE PROCEDURE `SP_OrdenTrabajoExterno_Crea`(IN p_PlacaBus VARCHAR(20),
    IN p_ContratoCO INT,
    IN p_Fecha DATETIME,
    IN p_ProveedorTE VARCHAR(20),
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_BusID INT;
    DECLARE v_BusActivo INT;
    DECLARE v_ContratoExistente INT;
    DECLARE v_ProveedorID INT;
    DECLARE v_ProveedorActivo INT;

    -- Obtener ID y estado del bus por su placa
    SELECT BusB, EstadoB INTO v_BusID, v_BusActivo 
    FROM Bus 
    WHERE NPlaca = p_PlacaBus 
    LIMIT 1;

    -- Verificar si el contrato existe
    SELECT COUNT(*) INTO v_ContratoExistente 
    FROM ContratoMantenimiento 
    WHERE CodigoCM = p_ContratoCO;

    -- Obtener ID y estado del proveedor por su nombre
    SELECT CodigoP, EstadoP INTO v_ProveedorID, v_ProveedorActivo 
    FROM Proveedor 
    WHERE Nombre = p_ProveedorTE 
    LIMIT 1;

    IF v_BusID IS NULL THEN
        SET p_Mensaje = 'El bus no existe';
    ELSEIF v_BusActivo = 0 THEN
        SET p_Mensaje = 'El bus está inactivo';
    ELSEIF v_ContratoExistente = 0 THEN
        SET p_Mensaje = 'El contrato de mantenimiento no existe';
    ELSEIF v_ProveedorID IS NULL THEN
        SET p_Mensaje = 'El proveedor no existe';
    ELSEIF v_ProveedorActivo = 0 THEN
        SET p_Mensaje = 'El proveedor está inactivo';
    ELSE
        INSERT INTO OrdenTrabajoExterno (
            CodigoBus, ContratoCO, Fecha, ProveedorTE, Estado
        ) VALUES (
            v_BusID, p_ContratoCO, p_Fecha, v_ProveedorID, 1
        );

        SET p_Mensaje = 'Orden de trabajo externa creada exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_OrdenTrabajoExterno_Inactiva
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_OrdenTrabajoExterno_Inactiva`;
delimiter ;;
CREATE PROCEDURE `SP_OrdenTrabajoExterno_Inactiva`(IN p_CodigoTE INT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    IF EXISTS (SELECT 1 FROM OrdenTrabajoExterno WHERE CodigoTE = p_CodigoTE AND Estado = 1) THEN
        UPDATE OrdenTrabajoExterno
        SET Estado = 0
        WHERE CodigoTE = p_CodigoTE;

        SET p_Mensaje = 'Orden de trabajo externa inactivada exitosamente';
    ELSE
        SET p_Mensaje = 'Orden externa no encontrada o ya está inactiva';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_OrdenTrabajoExterno_Lista
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_OrdenTrabajoExterno_Lista`;
delimiter ;;
CREATE PROCEDURE `SP_OrdenTrabajoExterno_Lista`()
BEGIN
    SELECT 
        ote.CodigoTE,
        ote.Fecha,
        ote.Estado,
        b.NPlaca,
        cm.CodigoCM,
        p.Nombre
    FROM OrdenTrabajoExterno ote
    INNER JOIN Bus b ON ote.CodigoBus = b.BusB
    INNER JOIN ContratoMantenimiento cm ON ote.ContratoCO = cm.CodigoCM
    INNER JOIN Proveedor p ON ote.ProveedorTE = p.CodigoP;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_OrdenTrabajoExterno_ObtenPorId
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_OrdenTrabajoExterno_ObtenPorId`;
delimiter ;;
CREATE PROCEDURE `SP_OrdenTrabajoExterno_ObtenPorId`(IN p_CodigoTE INT)
BEGIN
    SELECT 
        ote.CodigoTE,
        ote.Fecha,
        ote.Estado,
        b.NPlaca,
        cm.CodigoCM,
        p.Nombre
    FROM OrdenTrabajoExterno ote
    INNER JOIN Bus b ON ote.CodigoBus = b.BusB
    INNER JOIN ContratoMantenimiento cm ON ote.ContratoCO = cm.CodigoCM
    INNER JOIN Proveedor p ON ote.ProveedorTE = p.CodigoP
    WHERE ote.CodigoTE = p_CodigoTE;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_OrdenTrabajoInterno_Inactiva
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_OrdenTrabajoInterno_Inactiva`;
delimiter ;;
CREATE PROCEDURE `SP_OrdenTrabajoInterno_Inactiva`(IN p_CodigoTI INT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    IF EXISTS (SELECT 1 FROM OrdenTrabajoInterno WHERE CodigoTI = p_CodigoTI AND Estado = 1) THEN
        UPDATE OrdenTrabajoInterno
        SET Estado = 0
        WHERE CodigoTI = p_CodigoTI;

        SET p_Mensaje = 'Orden de trabajo interno inactivada exitosamente';
    ELSE
        SET p_Mensaje = 'Orden interna no encontrada o ya está inactiva';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Proveedor_Actualiza
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Proveedor_Actualiza`;
delimiter ;;
CREATE PROCEDURE `SP_Proveedor_Actualiza`(IN p_CodigoP INT,
    IN p_Nombre VARCHAR(100),
    IN p_Direccion VARCHAR(100),
    IN p_Telefono VARCHAR(20),
    IN p_Correo VARCHAR(50),
    IN p_EstadoP TINYINT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_ExisteCorreo INT;

    -- Validar si hay otro proveedor con el mismo correo
    SELECT COUNT(*) INTO v_ExisteCorreo
    FROM proveedor
    WHERE Correo = p_Correo AND CodigoP <> p_CodigoP;

    IF v_ExisteCorreo > 0 THEN
        SET p_Mensaje = 'El correo ya está registrado en otro proveedor';
    ELSE
        UPDATE proveedor
        SET Nombre = p_Nombre,
            Direccion = p_Direccion,
            Telefono = p_Telefono,
            Correo = p_Correo,
            EstadoP = p_EstadoP
        WHERE CodigoP = p_CodigoP;

        SET p_Mensaje = 'Proveedor actualizado exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Proveedor_Crea
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Proveedor_Crea`;
delimiter ;;
CREATE PROCEDURE `SP_Proveedor_Crea`(IN p_Nombre VARCHAR(100),
    IN p_Direccion VARCHAR(100),
    IN p_Telefono VARCHAR(20),
    IN p_Correo VARCHAR(50),
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_ExisteCorreo INT;

    -- Validar que no exista otro proveedor con el mismo correo
    SELECT COUNT(*) INTO v_ExisteCorreo
    FROM proveedor
    WHERE Correo = p_Correo;

    IF v_ExisteCorreo > 0 THEN
        SET p_Mensaje = 'El correo ya está registrado en otro proveedor';
    ELSE
        INSERT INTO proveedor (Nombre, Direccion, Telefono, Correo, EstadoP)
        VALUES (p_Nombre, p_Direccion, p_Telefono, p_Correo, 1);

        SET p_Mensaje = 'Proveedor creado exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Proveedor_Inactiva
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Proveedor_Inactiva`;
delimiter ;;
CREATE PROCEDURE `SP_Proveedor_Inactiva`(IN p_CodigoP INT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_EstadoActual TINYINT;

    -- Intentar obtener el estado actual del proveedor
    SELECT EstadoP INTO v_EstadoActual
    FROM proveedor
    WHERE CodigoP = p_CodigoP;

    -- Validar si se encontró el proveedor
    IF ROW_COUNT() = 0 THEN
        SET p_Mensaje = 'Proveedor no encontrado';
    ELSEIF v_EstadoActual = 0 THEN
        SET p_Mensaje = 'El proveedor ya está inactivo';
    ELSE
        UPDATE proveedor
        SET EstadoP = 0
        WHERE CodigoP = p_CodigoP;

        SET p_Mensaje = 'Proveedor inactivado exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Proveedor_Lista
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Proveedor_Lista`;
delimiter ;;
CREATE PROCEDURE `SP_Proveedor_Lista`()
BEGIN
    SELECT CodigoP, Nombre, Direccion, Telefono, Correo, EstadoP
    FROM proveedor;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Proveedor_ObtenPorId
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Proveedor_ObtenPorId`;
delimiter ;;
CREATE PROCEDURE `SP_Proveedor_ObtenPorId`(IN p_CodigoP INT)
BEGIN
    SELECT CodigoP, Nombre, Direccion, Telefono, Correo, EstadoP
    FROM proveedor
    WHERE CodigoP = p_CodigoP;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Repuesto_Actualiza
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Repuesto_Actualiza`;
delimiter ;;
CREATE PROCEDURE `SP_Repuesto_Actualiza`(IN p_CodigoR INT,                       -- Código del repuesto a actualizar
    IN p_NombreR VARCHAR(100),             -- Nuevo nombre del repuesto
    IN p_CategoriaR VARCHAR(100),          -- Nombre de la categoría
    IN p_MarcarepuestoR VARCHAR(100),      -- Descripción de la marca del repuesto
    IN p_ProveedorR VARCHAR(100),          -- Nombre del proveedor
    IN p_Precio DECIMAL(18,2),             -- Nuevo precio
    IN p_Estado TINYINT,                   -- Estado (1 = activo, 0 = inactivo)
    OUT p_Mensaje VARCHAR(255))
BEGIN
    -- Declaración de variables internas
    DECLARE v_ExisteNombre INT;
    DECLARE v_CategoriaID INT DEFAULT NULL;
    DECLARE v_MarcaID INT DEFAULT NULL;
    DECLARE v_ProveedorID INT DEFAULT NULL;

    -- Verificar si ya existe otro repuesto con el mismo nombre
    SELECT COUNT(*) INTO v_ExisteNombre 
    FROM Repuesto 
    WHERE NombreR = p_NombreR AND CodigoR <> p_CodigoR;

    -- Buscar el ID del proveedor por nombre
    SELECT CodigoP INTO v_ProveedorID 
    FROM Proveedor 
    WHERE Nombre = p_ProveedorR COLLATE utf8mb4_unicode_ci 
      AND EstadoP = 1
    LIMIT 1;

    -- Buscar el ID de la marca por descripción y proveedor
    SELECT CodigoMR INTO v_MarcaID 
    FROM Marcarepuesto 
    WHERE Descripcion = p_MarcarepuestoR COLLATE utf8mb4_unicode_ci 
      AND EstadoM = 1
      AND ProveedorMR = v_ProveedorID
    LIMIT 1;

    -- Buscar el ID de la categoría por nombre
    SELECT CodigoC INTO v_CategoriaID 
    FROM Categoria 
    WHERE NombreC = p_CategoriaR COLLATE utf8mb4_unicode_ci 
      AND EstadoC = 1
    LIMIT 1;

    -- Validaciones
    IF v_ExisteNombre > 0 THEN
        SET p_Mensaje = 'Ya existe otro repuesto con ese nombre';

    ELSEIF v_CategoriaID IS NULL OR v_MarcaID IS NULL OR v_ProveedorID IS NULL THEN
        SET p_Mensaje = 'Categoría, marca o proveedor no existe o están inactivos';

    ELSE
        -- Actualizar repuesto
        UPDATE Repuesto
        SET NombreR = p_NombreR,
            CategoriaR = v_CategoriaID,
            MarcarepuestoR = v_MarcaID,
            ProveedorR = v_ProveedorID,
            Precio = p_Precio,
            EstadoR = p_Estado
        WHERE CodigoR = p_CodigoR;

        SET p_Mensaje = 'Repuesto actualizado exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Repuesto_Crea
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Repuesto_Crea`;
delimiter ;;
CREATE PROCEDURE `SP_Repuesto_Crea`(IN p_NombreR VARCHAR(100),             -- Nombre del repuesto
    IN p_CategoriaR VARCHAR(100),          -- Nombre de la categoría
    IN p_MarcarepuestoR VARCHAR(100),      -- Descripción (nombre) de la marca del repuesto
    IN p_ProveedorR VARCHAR(100),          -- Nombre del proveedor
    IN p_Precio DECIMAL(18,2),             -- Precio del repuesto
    OUT p_Mensaje VARCHAR(255))
BEGIN
    -- Variables internas
    DECLARE v_ExisteNombre INT;
    DECLARE v_CategoriaID INT DEFAULT NULL;
    DECLARE v_MarcaID INT DEFAULT NULL;
    DECLARE v_ProveedorID INT DEFAULT NULL;

    -- Verificar si ya existe un repuesto con el mismo nombre
    SELECT COUNT(*) INTO v_ExisteNombre 
    FROM Repuesto
    WHERE NombreR = p_NombreR;

    -- Buscar el ID del proveedor por nombre
    SELECT CodigoP INTO v_ProveedorID 
    FROM Proveedor 
    WHERE Nombre = p_ProveedorR COLLATE utf8mb4_unicode_ci 
      AND EstadoP = 1
    LIMIT 1;

    -- Buscar el ID de la marca por descripción y proveedor
    SELECT CodigoMR INTO v_MarcaID 
    FROM Marcarepuesto 
    WHERE Descripcion = p_MarcarepuestoR COLLATE utf8mb4_unicode_ci 
      AND EstadoM = 1
      AND ProveedorMR = v_ProveedorID
    LIMIT 1;

    -- Buscar el ID de la categoría por nombre
    SELECT CodigoC INTO v_CategoriaID 
    FROM Categoria 
    WHERE NombreC = p_CategoriaR COLLATE utf8mb4_unicode_ci 
      AND EstadoC = 1
    LIMIT 1;

    -- Validaciones
    IF v_ExisteNombre > 0 THEN
        SET p_Mensaje = 'Ya existe un repuesto con ese nombre';

    ELSEIF v_CategoriaID IS NULL OR v_MarcaID IS NULL OR v_ProveedorID IS NULL THEN
        SET p_Mensaje = 'Categoría, marca o proveedor no existe o están inactivos';

    ELSE
        -- Insertar el nuevo repuesto
        INSERT INTO Repuesto (
            NombreR, CategoriaR, MarcarepuestoR, ProveedorR, Precio, EstadoR
        ) VALUES (
            p_NombreR, v_CategoriaID, v_MarcaID, v_ProveedorID, p_Precio, 1
        );

        SET p_Mensaje = 'Repuesto creado exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Repuesto_Inactiva
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Repuesto_Inactiva`;
delimiter ;;
CREATE PROCEDURE `SP_Repuesto_Inactiva`(IN p_CodigoR INT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_EstadoActual TINYINT;

    -- Intentar obtener el estado actual del repuesto
    SELECT EstadoR INTO v_EstadoActual
    FROM Repuesto
    WHERE CodigoR = p_CodigoR;

    -- Validar si se encontró el repuesto
    IF ROW_COUNT() = 0 THEN
        SET p_Mensaje = 'Repuesto no encontrado';
    ELSEIF v_EstadoActual = 0 THEN
        SET p_Mensaje = 'El repuesto ya está inactivo';
    ELSE
        UPDATE Repuesto SET EstadoR = 0 WHERE CodigoR = p_CodigoR;
        SET p_Mensaje = 'Repuesto inactivado exitosamente';
    END IF;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Repuesto_Lista
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Repuesto_Lista`;
delimiter ;;
CREATE PROCEDURE `SP_Repuesto_Lista`()
BEGIN
    SELECT 
        r.CodigoR,
        r.NombreR,
        c.NombreC AS NombreC,
        m.Descripcion AS Descripcion,
        p.Nombre AS Nombre,
        r.Precio,
        r.EstadoR
    FROM Repuesto r
    INNER JOIN Categoria c ON r.CategoriaR = c.CodigoC
    INNER JOIN Marcarepuesto m ON r.MarcarepuestoR = m.CodigoMR
    INNER JOIN Proveedor p ON r.ProveedorR = p.CodigoP;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Repuesto_ObtenPorId
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Repuesto_ObtenPorId`;
delimiter ;;
CREATE PROCEDURE `SP_Repuesto_ObtenPorId`(IN p_CodigoR INT)
BEGIN
    SELECT 
        r.CodigoR,
        r.NombreR,
        r.CategoriaR,
        c.NombreC,
        r.MarcarepuestoR,
        m.Descripcion,
        r.ProveedorR,
        p.Nombre,
        r.Precio,
        r.EstadoR
    FROM Repuesto r
    INNER JOIN Categoria c ON r.CategoriaR = c.CodigoC
    INNER JOIN Marcarepuesto m ON r.MarcarepuestoR = m.CodigoMR
    INNER JOIN Proveedor p ON r.ProveedorR = p.CodigoP
    WHERE r.CodigoR = p_CodigoR;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Usuario_ObtenPorId
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Usuario_ObtenPorId`;
delimiter ;;
CREATE PROCEDURE `SP_Usuario_ObtenPorId`(IN u_Usu_Id INT)
BEGIN
  SELECT Usu_Id, Usu_Nombre, Usu_Rol FROM usuario
  WHERE Usu_Id = u_Usu_Id; 
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for SP_Usuario_Validar
-- ----------------------------
DROP PROCEDURE IF EXISTS `SP_Usuario_Validar`;
delimiter ;;
CREATE PROCEDURE `SP_Usuario_Validar`(IN u_Nombre VARCHAR(100),
  IN u_Password VARCHAR(100))
BEGIN
  SELECT usu.Usu_Id, usu.Usu_Nombre, usu.Usu_Rol 
  FROM Usuario usu
  WHERE usu.Usu_Nombre = u_Nombre AND usu.Usu_Password = u_Password;
END
;;
delimiter ;

-- ----------------------------
-- Procedure structure for _Navicat_Temp_Stored_Proc
-- ----------------------------
DROP PROCEDURE IF EXISTS `_Navicat_Temp_Stored_Proc`;
delimiter ;;
CREATE PROCEDURE `_Navicat_Temp_Stored_Proc`(IN p_OrdenTrabajoInternoID INT,
    IN p_CodigoRepu VARCHAR(40),      -- Aquí recibes el nombre del repuesto
    IN p_MecanicoTI VARCHAR(40),      -- Aquí recibes el nombre del mecánico
    IN p_Parte VARCHAR(40),
    IN p_Pieza VARCHAR(40),
    IN p_Cantidad INT,
    OUT p_Mensaje VARCHAR(255))
BEGIN
    DECLARE v_OrdenExistente INT;
    DECLARE v_RepuestoID INT;
    DECLARE v_MecanicoID INT;

    -- Verificar si la orden de trabajo interna existe
    SELECT COUNT(*) INTO v_OrdenExistente 
    FROM OrdenTrabajoInterno 
    WHERE CodigoTI = p_OrdenTrabajoInternoID;

    -- Obtener el ID del repuesto
    SELECT CodigoR INTO v_RepuestoID 
    FROM Repuesto 
    WHERE NombreR = p_CodigoRepu 
    LIMIT 1;

    -- Obtener el ID del mecánico
    SELECT CodigoM INTO v_MecanicoID 
    FROM Mecanico 
    WHERE Nombre = p_MecanicoTI 
    LIMIT 1;

    -- Validaciones
    IF v_OrdenExistente = 0 THEN
        SET p_Mensaje = 'La orden de trabajo interna no existe';
    ELSEIF v_RepuestoID IS NULL THEN
        SET p_Mensaje = 'El repuesto no existe';
    ELSEIF v_MecanicoID IS NULL THEN
        SET p_Mensaje = 'El mecánico no existe';
    ELSE
        -- Insertar el detalle con los IDs correctos
        INSERT INTO DetalleOTI (
            OrdenTrabajoInternoID, CodigoRepu, MecanicoTI, Parte, Pieza, Cantidad
        ) VALUES (
            p_OrdenTrabajoInternoID, v_RepuestoID, v_MecanicoID, p_Parte, p_Pieza, p_Cantidad
        );

        SET p_Mensaje = 'Detalle de orden de trabajo interno creado exitosamente';
    END IF;
END
;;
delimiter ;

SET FOREIGN_KEY_CHECKS = 1;
