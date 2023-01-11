import axios from "axios";
import React, { useState } from "react";
import { Button, Form, FormControl, Modal } from "react-bootstrap";
import { useNavigate } from "react-router-dom";

const ChangeAnswerModal = (props) => {
  const [innhold, setInnhold] = useState(props.svarInnhold);
  const [errorMessageStatus, setErrorMessageStatus] = useState("none");

  const navigate = useNavigate();

  const onChangeSvar = (event) => {
    const inputValue = event.target.value;

    setInnhold(inputValue);
  };

  const endreSvar = (event) => {
    const innholdWithoutSpace = innhold.trim();
    if (innholdWithoutSpace.trim().length === 0) {
      setErrorMessageStatus("block");
    } else {
      const endretSvar = {
        svarId: props.svarId,
        innhold: innholdWithoutSpace,
        innleggId: props.innleggId,
        dato: props.svarDato,
        brukernavn: props.sessionBrukernavn,
      };
      axios
        .post("/endresvar", endretSvar)
        .then(() => {
          props.updateAnswer(endretSvar);
          props.closeModalChange();
        })
        .catch((error) => {
          if (error.response.status === 401) {
            navigate("/login");
          }
        });
    }

    event.preventDefault();
  };

  return (
    <>
      <Modal
        show={props.showModalChange}
        onHide={props.closeModalChange}
        animation={false}
      >
        <Modal.Header>
          <Modal.Title>Endre svar</Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <Form onSubmit={endreSvar}>
            <FormControl type="text" value={innhold} onChange={onChangeSvar} />
          </Form>
          <div className={`invalid-feedback d-${errorMessageStatus}`}>
            Svar må fylles ut
          </div>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={props.closeModalChange}>
            Avbryt
          </Button>
          <Button variant="primary" onClick={endreSvar}>
            Endre
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
};

export default ChangeAnswerModal;
