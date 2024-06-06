import { baseUrl } from "../config";
import Request from "../base/requests";
import HttpMethod from "../base/methods";
import { promotionsRoutes } from "../routes";
import SendDto from "../base/sendDto";

const request = new Request();
const httpMethod = new HttpMethod();

const getAll = async (data) => {
  const sendDto = new SendDto(
    httpMethod.get(),
    `${baseUrl}${promotionsRoutes.getAll}`
  );

  return await request.send(sendDto);
};

export { getAll };
