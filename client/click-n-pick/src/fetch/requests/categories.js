import { baseUrl } from "../config";
import Request from "../base/requests";
import HttpMethod from "../base/methods";
import { categoriesRoutes } from "../routes";
import SendDto from "../base/sendDto";

const request = new Request();
const httpMethod = new HttpMethod();

const getAll = async (data) => {
  const sendDto = new SendDto(
    httpMethod.get(),
    `${baseUrl}${categoriesRoutes.getAll}`,
    data
  );

  return await request.send(sendDto);
};

const getAllForAdmin = async (data) => {
  const sendDto = new SendDto(
    httpMethod.get(),
    `${baseUrl}${categoriesRoutes.getAllForAdmin}`,
    data
  );

  return await request.send(sendDto);
};

const getById = async (data) => {
  const sendDto = new SendDto(
    httpMethod.get(),
    `${baseUrl}${categoriesRoutes.getById}/${data}`
  );

  return await request.send(sendDto);
};

const edit = async (data) => {
  const sendDto = new SendDto(
    httpMethod.post(),
    `${baseUrl}${categoriesRoutes.edit}`,
    data
  );

  return await request.send(sendDto);
};

const deleteCategory = async (data) => {
  const sendDto = new SendDto(
    httpMethod.post(),
    `${baseUrl}${categoriesRoutes.delete}`,
    data
  );

  return await request.send(sendDto);
};

const createCategory = async (data) => {
  const sendDto = new SendDto(
    httpMethod.post(),
    `${baseUrl}${categoriesRoutes.create}`,
    data
  );

  return await request.send(sendDto);
};

export {
  getAll,
  getAllForAdmin,
  getById,
  edit,
  deleteCategory,
  createCategory,
};
