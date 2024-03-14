"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
const connection_1 = require("./connection");
const promises_1 = __importDefault(require("fs/promises"));
const extractData = () => __awaiter(void 0, void 0, void 0, function* () {
    // get admin data
    const adminDataResult = yield connection_1.pool.query('SELECT * FROM admin');
    const adminData = JSON.stringify(adminDataResult.rows, null, 2);
    yield promises_1.default.writeFile('data/adminData.json', adminData);
    console.log('Admin data extracted');
    // get business info data
    const businessInfoResult = yield connection_1.pool.query('SELECT * FROM business_info');
    const businessInfo = JSON.stringify(businessInfoResult.rows, null, 2);
    yield promises_1.default.writeFile('data/businessInfo.json', businessInfo);
    console.log('Business info extracted');
    connection_1.pool.end();
    console.log('Connection closed');
    return;
});
extractData();
