import { baseUrl } from "../config";
import Request from "../base/requests";
import HttpMethod from "../base/methods";
import { productsRoutes } from "../routes";
import SendDto from "../base/sendDto";

const request = new Request();
const httpMethod = new HttpMethod();

const getProduct = async (data) => {
  const sendDto = new SendDto(
    httpMethod.get(),
    `${baseUrl}${productsRoutes.getProductById}/${data}`
  );

  return await request.send(sendDto);
};

const createProduct = async (data, isSendDataForm) => {
  const sendDto = new SendDto(
    httpMethod.post(),
    `${baseUrl}${productsRoutes.create}`,
    data,
    true,
    isSendDataForm
  );

  return await request.send(sendDto);
};

const myProducts = async (data) => {
  const sendDto = new SendDto(
    httpMethod.get(),
    `${baseUrl}${productsRoutes.myProducts}${data ? `?${data}` : ""}`
  );

  return await request.send(sendDto);
};

const deleteProduct = async (data) => {
  const sendDto = new SendDto(
    httpMethod.post(),
    `${baseUrl}${productsRoutes.delete}`,
    data
  );

  return await request.send(sendDto);
};

const getEditDetails = async (data) => {
  const sendDto = new SendDto(
    httpMethod.get(),
    `${baseUrl}${productsRoutes.getEditDetails}/${data}`
  );

  return await request.send(sendDto);
};

const editProduct = async (data, isSendDataForm) => {
  const sendDto = new SendDto(
    httpMethod.post(),
    `${baseUrl}${productsRoutes.edit}`,
    data,
    true,
    isSendDataForm
  );

  return await request.send(sendDto);
};

const promoteProduct = async (data) => {
  const sendDto = new SendDto(
    httpMethod.post(),
    `${baseUrl}${productsRoutes.promote}`,
    data
  );

  return await request.send(sendDto);
};

const searchAll = async (data) => {
  const sendDto = new SendDto(
    httpMethod.get(),
    `${baseUrl}${productsRoutes.search}${data ? `?${data}` : ""}`
  );

  return await request.send(sendDto);
};

const productDetails = async (data) => {
  const sendDto = new SendDto(
    httpMethod.get(),
    `${baseUrl}${productsRoutes.details}/${data}`
  );

  return await request.send(sendDto);
};

const userProducts = async (data) => {
  const sendDto = new SendDto(
    httpMethod.get(),
    `${baseUrl}${productsRoutes.userProducts}${data ? `?${data}` : ""}`
  );

  return await request.send(sendDto);
};

export {
  getProduct,
  createProduct,
  myProducts,
  deleteProduct,
  getEditDetails,
  editProduct,
  promoteProduct,
  searchAll,
  productDetails,
  userProducts,
};
