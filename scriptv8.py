from ultralytics import YOLO
import cv2
import matplotlib.pyplot as plt

# Load a pretrained YOLO model
model = YOLO('Assets/yolov8/best.pt')

# Perform object detection on an image using the model
image_path = 'C:/Users/opria/Pictures/220df0d70_jpg.rf.1bf3171e65d6c1a59d99e58d5893b79b.jpg'
results = model(image_path)

# Convert results to OpenCV format and draw bounding boxes
image = cv2.imread(image_path)
with open('coord.txt', 'w') as f:
    for result in results:
        for box in result.boxes:
            x1, y1, x2, y2 = map(int, box.xyxy[0])  # Convert to int
            confidence = box.conf[0]
            class_id = box.cls[0]

            # Draw bounding box
            cv2.rectangle(image, (x1, y1), (x2, y2), (0, 255, 0), 2)

            # Put class id and confidence
            label = f'{class_id}: {confidence:.2f}'
            cv2.putText(image, label, (x1, y1 - 10), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0, 255, 0), 2)
            f.write(f'ship {confidence:.2f} {x1} {y1} {x2} {y2}\n')

print(f'Coordinates saved to coord.txt')

# Convert BGR image to RGB
image_rgb = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)

# Show the image using matplotlib
plt.imshow(image_rgb)
plt.axis('off')  # Turn off axis numbers and ticks
plt.show()
