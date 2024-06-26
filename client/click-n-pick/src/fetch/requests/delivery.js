import { baseUrl } from "../config";
import Request from "../base/requests";
import HttpMethod from "../base/methods";
import { deliveryRoutes } from "../routes";
import SendDto from "../base/sendDto";

const request = new Request();
const httpMethod = new HttpMethod();

const requestShipment = async (data) => {
  const sendDto = new SendDto(
    httpMethod.post(),
    `${baseUrl}${deliveryRoutes.requestShipment}`,
    data
  );

  return await request.send(sendDto);
};

const cancelShipment = async (data) => {
  const sendDto = new SendDto(
    httpMethod.post(),
    `${baseUrl}${deliveryRoutes.cancelShipment}`,
    data
  );

  return await request.send(sendDto);
};

const declineShipment = async (data) => {
  const sendDto = new SendDto(
    httpMethod.post(),
    `${baseUrl}${deliveryRoutes.declineShipment}`,
    data
  );

  return await request.send(sendDto);
};

const getShipmentDetails = async (data) => {
  const sendDto = new SendDto(
    httpMethod.get(),
    `${baseUrl}${deliveryRoutes.getShipmentDetails}/${data}`
  );

  return await request.send(sendDto);
};

const acceptShipment = async (data) => {
  const sendDto = new SendDto(
    httpMethod.post(),
    `${baseUrl}${deliveryRoutes.acceptShipment}`,
    data
  );

  return await request.send(sendDto);
};

const shipmentsToSend = async (data) => {
  const sendDto = new SendDto(
    httpMethod.get(),
    `${baseUrl}${deliveryRoutes.getShipmentsToSend}`
  );

  return await request.send(sendDto);
};

const getCities = async (data) => {
  const sendDto = new SendDto(
    httpMethod.get(),
    `${baseUrl}${deliveryRoutes.getCities}`
  );

  return await request.send(sendDto);
};

const getQuarters = async (data) => {
  const sendDto = new SendDto(
    httpMethod.get(),
    `${baseUrl}${deliveryRoutes.getQuarters}?${data}`
  );

  return await request.send(sendDto);
};

const getStreets = async (data) => {
  const sendDto = new SendDto(
    httpMethod.get(),
    `${baseUrl}${deliveryRoutes.getStreets}?${data}`
  );

  return await request.send(sendDto);
};

const shipmentsToReceive = async (data) => {
  const sendDto = new SendDto(
    httpMethod.get(),
    `${baseUrl}${deliveryRoutes.getShipmentsToReceive}`
  );

  return await request.send(sendDto);
};

export {
  requestShipment,
  shipmentsToSend,
  shipmentsToReceive,
  acceptShipment,
  getCities,
  getQuarters,
  getStreets,
  cancelShipment,
  declineShipment,
  getShipmentDetails,
};
