import { pool } from "./connection"
import fs from 'fs/promises';

const extractData = async () => {

  // get admin data
  const adminDataResult = await pool.query('SELECT * FROM admin');
  const adminData = JSON.stringify(adminDataResult.rows, null, 2);
  await fs.writeFile('data/adminData.json', adminData);
  console.log('Admin data extracted');

  // get business info data
  const businessInfoResult = await pool.query('SELECT * FROM business_info');
  const businessInfo = JSON.stringify(businessInfoResult.rows, null, 2);
  await fs.writeFile('data/businessInfo.json', businessInfo);
  console.log('Business info extracted');

  pool.end();

  console.log('Connection closed');
  return;
}

extractData();
