class SendDto {
  constructor(method, path, body, isFormData = false, isSendDataForm = false) {
    this.#validate(method, path);
    this.method = method;
    this.path = path;
    this.body = body;
    this.isFormData = isFormData;
    this.isSendDataForm = isSendDataForm;
  }

  #validate(method, path) {
    if (!method || !path) {
      console.error("Request must have method and path");
      throw new Error("Request must have method and path");
    }
  }
}

export default SendDto;
