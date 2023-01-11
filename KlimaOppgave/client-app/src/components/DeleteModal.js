import React from "react";
import { Button, Modal } from "react-bootstrap";

const DeleteModal = (props) => {
  const onDelete = () => {
    props.onDelete();
    props.closeModalDelete();
  };
  return (
    <>
      <Modal
        show={props.showModalDelete}
        onHide={props.closeModalDelete}
        animation={false}
      >
        <Modal.Body>
          <div className="fs-5 text">{props.sentence}</div>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={props.closeModalDelete}>
            Avbryt
          </Button>
          <Button variant="danger" onClick={onDelete}>
            Slett
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
};

export default DeleteModal;
