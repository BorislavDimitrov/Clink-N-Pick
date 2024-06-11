import { baseUrl } from "../config";
import Request from "../base/requests";
import HttpMethod from "../base/methods";
import { deliveryRoutes } from "../routes";
import SendDto from "../base/sendDto";

const request = new Request();
const httpMethod = new HttpMethod();

// const createLabel = async (data) => {
//   const sendDto = new SendDto(
//     httpMethod.post(),
//     `${baseUrl}${deliveryRoutes.createLabel}`,
//     data
//   );

//   return await request.send(sendDto);
// };

const requestShipment = async (data) => {
  const sendDto = new SendDto(
    httpMethod.post(),
    `${baseUrl}${deliveryRoutes.requestShipment}`,
    data
  );

  return await request.send(sendDto);
};

const shipmentsToSend = async (data) => {
  const sendDto = new SendDto(
    httpMethod.get(),
    `${baseUrl}${deliveryRoutes.shipmentsToSend}}`
  );

  return await request.send(sendDto);
};

const shipmentsToReceive = async (data) => {
  const sendDto = new SendDto(
    httpMethod.get(),
    `${baseUrl}${deliveryRoutes.ShipmentsToReceive}}`
  );

  return await request.send(sendDto);
};

export { requestShipment, shipmentsToSend, shipmentsToReceive };
