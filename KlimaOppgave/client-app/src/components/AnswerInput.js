import axios from "axios";
import React, { useState } from "react";

const AnswerInput = (props) => {
  const [svarText, setSvarText] = useState("");

  const onChangeText = (event) => {
    setSvarText(event.target.value);
  };

  const submitForm = (event) => {
    const svar = {
      innhold: svarText,
      innleggId: props.innleggId,
    };

    axios.post("/leggsvar", svar).then((response) => {
      svar.svarId = response.data.svarId;
      svar.dato = response.data.dato;
      props.addAnswer(svar, svar.innleggId);
    });

    setSvarText("");

    event.preventDefault();
  };

  return (
    <div className="card-footer">
      <form onSubmit={submitForm} className="input-group">
        <input
          value={svarText}
          onChange={onChangeText}
          type="text"
          className="form-control"
          placeholder="Legg inn et svar"
        />
        <button className="btn btn-primary" type="button" onClick={submitForm}>
          Svar
        </button>
      </form>
    </div>
  );
};

export default AnswerInput;
