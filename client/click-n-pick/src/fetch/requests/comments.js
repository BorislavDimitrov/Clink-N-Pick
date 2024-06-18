import { baseUrl } from "../config";
import Request from "../base/requests";
import HttpMethod from "../base/methods";
import { commentsRoutes } from "../routes";
import SendDto from "../base/sendDto";

const request = new Request();
const httpMethod = new HttpMethod();

const createComment = async (data) => {
  const sendDto = new SendDto(
    httpMethod.post(),
    `${baseUrl}${commentsRoutes.createComment}`,
    data
  );

  return await request.send(sendDto);
};

const getForProduct = async (data) => {
  const sendDto = new SendDto(
    httpMethod.get(),
    `${baseUrl}${commentsRoutes.getForProduct}/${data}`
  );

  return await request.send(sendDto);
};

export { createComment, getForProduct };
