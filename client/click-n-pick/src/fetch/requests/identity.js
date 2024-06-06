import { baseUrl } from "../config";
import Request from "../base/requests";
import HttpMethod from "../base/methods";
import { identityRoutes } from "../routes";
import SendDto from "../base/sendDto";

const request = new Request();
const httpMethod = new HttpMethod();

const register = async (data) => {
  const sendDto = new SendDto(
    httpMethod.post(),
    `${baseUrl}${identityRoutes.register}`,
    data
  );

  return await request.send(sendDto);
};

const confirmEmail = async (data) => {
  const sendDto = new SendDto(
    httpMethod.post(),
    `${baseUrl}${identityRoutes.confirmEmail}`,
    data
  );

  return await request.send(sendDto);
};

const login = async (data) => {
  const sendDto = new SendDto(
    httpMethod.post(),
    `${baseUrl}${identityRoutes.login}`,
    data
  );

  return await request.send(sendDto);
};

const forgotPasswordInput = async (data) => {
  const sendDto = new SendDto(
    httpMethod.post(),
    `${baseUrl}${identityRoutes.forgotPasswordInput}`,
    data
  );

  return await request.send(sendDto);
};

const forgotPasswordChange = async (data) => {
  const sendDto = new SendDto(
    httpMethod.post(),
    `${baseUrl}${identityRoutes.forgotPasswordChange}`,
    data
  );

  return await request.send(sendDto);
};

const passwordChange = async (data) => {
  const sendDto = new SendDto(
    httpMethod.post(),
    `${baseUrl}${identityRoutes.passwordChange}`,
    data
  );

  return await request.send(sendDto);
};

export {
  register,
  confirmEmail,
  login,
  forgotPasswordInput,
  forgotPasswordChange,
  passwordChange,
};
