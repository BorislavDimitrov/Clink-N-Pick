import { baseUrl } from "../config";
import Request from "../base/requests";
import HttpMethod from "../base/methods";
import { paymentsRoutes } from "../routes";
import SendDto from "../base/sendDto";

const request = new Request();
const httpMethod = new HttpMethod();

const createPaymentIntent = async (data) => {
  const sendDto = new SendDto(
    httpMethod.post(),
    `${baseUrl}${paymentsRoutes.createPaymentIntent}`,
    data
  );

  return await request.send(sendDto);
};

export { createPaymentIntent };
