import { pool } from "./connection";
import fs from 'fs/promises';

const seedData = async () => {
  // clear existing data
  await pool.query('DELETE FROM admin');
  await pool.query('DELETE FROM business_info');
  await pool.query('DELETE FROM jobs_images');
  await pool.query('DELETE FROM jobs');
  await pool.query('DELETE FROM job_types');

  // insert admin data
  const adminData = await fs.readFile('data/adminData.json', 'utf-8');
  const parsedAdminData = JSON.parse(adminData);
  for (let admin of parsedAdminData) {
    await pool.query('INSERT INTO admin (admin_id, name, password, role) VALUES ($1, $2, $3)', [admin.admin_id, admin.name, admin.password, admin.role]);
  }
  console.log('Admin data inserted');

  // insert business info data
  const businessInfo = await fs.readFile('data/businessInfo.json', 'utf-8');
  const parsedBusinessInfo = JSON.parse(businessInfo);
  for (let business of parsedBusinessInfo) {
    await pool.query(
      `INSERT INTO business_info (info_id, name, email, phone, address, city, info, logo, facebook, instagram, youtube, tiktok, linkedin)
      VALUES ($1, $2, $3, $4, $5, $6, $7, $8, $9, $10, $11, $12, $13)`
      , [business.info_id, business.name, business.email, business.phone, business.address, business.city, business.info, business.logo, business.facebook, business.instagram, business.youtube, business.tiktok, business.linkedin]);
  }
  console.log('Business info inserted');

  // insert jobs images data
  const jobsImages = await fs.readFile('data/jobsImages.json', 'utf-8');
  const parsedJobsImages = JSON.parse(jobsImages);
  for (let image of parsedJobsImages) {
    await pool.query('INSERT INTO jobs_images (image_id, job_id, image) VALUES ($1, $2, $3)', [image.image_id, image.job_id, image.image]);
  }
  console.log('Jobs images inserted');

  // insert jobs data
  const jobs = await fs.readFile('data/jobs.json', 'utf-8');
  const parsedJobs = JSON.parse(jobs);
  for (let job of parsedJobs) {
    await pool.query(
      `INSERT INTO jobs (job_id, title, tagline, description, jov_type, date, client, location)
      VALUES ($1, $2, $3, $4, $5, $6, $7, $8)`
      , [job.job_id, job.title, job.tagline, job.description, job.job_type, job.date, job.client, job.location]);
  }
  console.log('Jobs inserted');

  // insert job types data
  const jobTypes = await fs.readFile('data/jobTypes.json', 'utf-8');
  const parsedJobTypes = JSON.parse(jobTypes);
  for (let jobType of parsedJobTypes) {
    await pool.query('INSERT INTO job_types (name, description, image, icon) VALUES ($1, $2, $3, $4)', [jobType.name, jobType.description, jobType.image, jobType.icon]);
  }
  console.log('Job types inserted');

  pool.end();
  console.log('Connection closed');
  return;
}

seedData();
