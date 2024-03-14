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

  // jobs images data
  const jobsImagesResult = await pool.query('SELECT * FROM jobs_images');
  const jobsImages = JSON.stringify(jobsImagesResult.rows, null, 2);
  await fs.writeFile('data/jobsImages.json', jobsImages);
  console.log('Jobs images extracted');

  // jobs data
  const jobsResult = await pool.query('SELECT * FROM jobs');
  const jobs = JSON.stringify(jobsResult.rows, null, 2);
  await fs.writeFile('data/jobs.json', jobs);
  console.log('Jobs extracted');

  // job types data
  const jobTypesResult = await pool.query('SELECT * FROM job_types');
  const jobTypes = JSON.stringify(jobTypesResult.rows, null, 2);
  await fs.writeFile('data/jobTypes.json', jobTypes);
  console.log('Job types extracted');

  pool.end();

  console.log('Connection closed');
  return;
}

extractData();
