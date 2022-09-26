import { Container, Row, Col, Card } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import {
  faFolderClosed,
  faUser,
  faHardDrive,
} from "@fortawesome/free-regular-svg-icons";
import { faWordpress } from "@fortawesome/free-brands-svg-icons";
import { Link } from "react-router-dom";

const Dashboard = () => {
  return (
    <Container>
      <Row>
        <Col>
          <Link to="/word-press">
            <Card className="text-center">
              <Card.Body>
                <FontAwesomeIcon icon={faWordpress} size="xl" /> WordPress Blogs
              </Card.Body>
            </Card>
          </Link>
        </Col>
        <Col>
          <Link to="/file-manager">
            <Card className="text-center">
              <Card.Body>
                <FontAwesomeIcon icon={faFolderClosed} size="xl" /> File Manager
              </Card.Body>
            </Card>
          </Link>
        </Col>
        <Col>
          <Link to="/databases">
            <Card className="text-center">
              <Card.Body>
                <FontAwesomeIcon icon={faHardDrive} size="xl" /> Databases
              </Card.Body>
            </Card>
          </Link>
        </Col>
        <Col>
          <Link to="/users">
            <Card className="text-center">
              <Card.Body>
                <FontAwesomeIcon icon={faUser} size="xl" /> Users
              </Card.Body>
            </Card>
          </Link>
        </Col>
      </Row>
    </Container>
  );
};

export default Dashboard;
