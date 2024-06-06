import { baseUrl } from "../config";
import Request from "../base/requests";
import HttpMethod from "../base/methods";
import { usersRoutes } from "../routes";
import SendDto from "../base/sendDto";

const request = new Request();
const httpMethod = new HttpMethod();

const getEditProfileInfo = async () => {
  const sendDto = new SendDto(
    httpMethod.get(),
    `${baseUrl}${usersRoutes.editProfile}`
  );

  return await request.send(sendDto);
};

const editProfile = async (data, isSendDataForm) => {
  const sendDto = new SendDto(
    httpMethod.post(),
    `${baseUrl}${usersRoutes.editProfile}`,
    data,
    true,
    isSendDataForm
  );

  return await request.send(sendDto);
};

const viewProfile = async (data) => {
  const sendDto = new SendDto(
    httpMethod.get(),
    `${baseUrl}${usersRoutes.viewProfile}/${data}`
  );

  return await request.send(sendDto);
};

export { getEditProfileInfo, editProfile, viewProfile };
