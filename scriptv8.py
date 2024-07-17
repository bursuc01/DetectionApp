from ultralytics import YOLO
import cv2

# Load a pretrained YOLO model
model = YOLO('C:/Users/opria/OneDrive/Documents/Licenta/DetectionApp/Assets/yolov8/best.pt')

# Perform object detection on an image using the model
image_path = 'C:/Users/opria/OneDrive/Documents/Licenta/DetectionApp/Assets/source.jpg'
results = model(image_path)

# Convert results to OpenCV format and draw bounding boxes
image = cv2.imread(image_path)
for result in results:
    for box in result.boxes:
        x1, y1, x2, y2 = map(int, box.xyxy[0])  # Convert to int
        confidence = box.conf[0]
        class_id = box.cls[0]
        print(f'ship {confidence:.2f} {x1} {y1} {x2} {y2}')


